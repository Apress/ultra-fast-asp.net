using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class async_seq : System.Web.UI.Page
{
    public const string ConnString = "Data Source=.;Integrated Security=True;Async=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, null, null, true);
        RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("waitfor delay '00:00:01'", conn);
        IAsyncResult ar = cmd.BeginExecuteNonQuery(cb, cmd);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                int rows = cmd.EndExecuteNonQuery(ar);
            }
        }
        PageAsyncTask pat = new PageAsyncTask(BeginAsync2, EndAsync2, null, null, true);
        this.RegisterAsyncTask(pat);
        this.ExecuteRegisteredAsyncTasks();
    }

    private IAsyncResult BeginAsync2(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("waitfor delay '00:00:01'", conn);
        IAsyncResult ar = cmd.BeginExecuteNonQuery(cb, cmd);
        return ar;
    }

    private void EndAsync2(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                int rows = cmd.EndExecuteNonQuery(ar);
            }
        }
    }
}
