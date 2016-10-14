using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Transactions;

public static class BrokerWorker
{
    public const string ConnString = 
        "Data Source=.;Initial Catalog=Sample;Integrated Security=True";

    public static void Work()
    {
        DateTime lastLogTime = DateTime.Now;
        string lastMessage = String.Empty;

        for (; ; )
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[ReceiveTaskRequest]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;       // seconds
                        cmd.Parameters.Add("msg", SqlDbType.VarBinary, -1).Direction = 
                            ParameterDirection.Output;
                        byte[] msg = null;
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            msg = cmd.Parameters["msg"].Value as byte[];
                            if (msg != null)
                            {
                                PerformTask(msg);
                            }
                        }
                        catch (Exception e)
                        {
                            if (e is ThreadAbortException)
                            {
                                break;
                            }
                            else
                            {
                                TimeSpan elapsed = DateTime.Now - lastLogTime;
                                if ((lastMessage != e.Message) || 
                                    (elapsed.Minutes > 10))
                                {
                                    EventLog.WriteEntry("Application", e.Message, 
                                        EventLogEntryType.Error, 105);
                                    lastLogTime = DateTime.Now;
                                }
                                else if (lastMessage == e.Message)
                                {
                                    Thread.Sleep(60000);
                                }
                                lastMessage = e.Message;
                            }
                        }
                        finally
                        {
                            if (msg != null)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
            }
        }
    }

    //
    // If you don't have an SMTP server on your local machine,
    // update "localhost" below accordingly.
    //
    private static void PerformTask(byte[] msg)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(msg))
        {
            TaskRequest request = formatter.Deserialize(stream) as TaskRequest;
            if (request != null)
            {
                switch (request.TaskType)
                {
                    case TaskTypeEnum.Email:
                        SmtpClient smtp = new SmtpClient("localhost");
                        smtp.Send("user@example.com", request.EmailToAddress,
                            request.EmailSubject, request.EmailMesssage);
                        break;
                }
            }
        }
    }
}
