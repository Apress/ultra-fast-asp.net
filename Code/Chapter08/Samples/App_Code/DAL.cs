using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Samples
{
    public class DAL : ADAL
    {
        public override IAsyncResult AddBrowserBegin(RequestInfo info,
            AsyncCallback callback)
        {
            SqlConnection conn =
                new SqlConnection(ConfigData.TrafficConnectionStringAsync);
            SqlCommand cmd = new SqlCommand("[Traffic].[AddBrowser]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = info.BrowserId;
            cmd.Parameters.Add("agent", SqlDbType.VarChar, 256).Value = info.Agent;
            conn.Open();
            return cmd.BeginExecuteNonQuery(callback, cmd);
        }

        public override void AddBrowserEnd(IAsyncResult ar)
        {
            using (SqlCommand cmd = ar.AsyncState as SqlCommand)
            {
                if (cmd != null)
                {
                    try
                    {
                        cmd.EndExecuteNonQuery(ar);
                    }
                    catch (SqlException e)
                    {
                        EventLog.WriteEntry("Application",
                            "Error in AddBrowser: " + e.Message,
                            EventLogEntryType.Error, 103);
                        throw;
                    }
                    finally
                    {
                        cmd.Connection.Dispose();
                    }
                }
            }
        }
    }
}