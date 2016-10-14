using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;

public partial class broker_email : Page
{
    public const string ConnString = 
        "Data Source=.;Initial Catalog=Sample;Integrated Security=True;Async=True";

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (this.IsPostBack)
        {
            PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, null, null, true);
            RegisterAsyncTask(pat);
        }
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        TaskRequest request = new TaskRequest()
        {
            TaskType = TaskTypeEnum.Email,
            EmailToAddress = this.Email.Text,
            EmailSubject = this.Subject.Text,
            EmailMesssage = this.Body.Text
        };
        SqlConnection conn = new SqlConnection(ConnString);
        SqlCommand cmd = new SqlCommand("[dbo].[SendTaskRequest]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, request);
            stream.Flush();
            cmd.Parameters.Add("msg", SqlDbType.VarBinary).Value = stream.ToArray();
        }
        conn.Open();
        IAsyncResult ar = cmd.BeginExecuteNonQuery(cb, cmd);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                cmd.EndExecuteNonQuery(ar);
                this.Status.Text = "Message sent";
            }
        }
    }
}
