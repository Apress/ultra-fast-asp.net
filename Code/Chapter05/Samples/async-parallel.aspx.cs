using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class async_parallel : System.Web.UI.Page
{
    public const string ConnString = "Data Source=.;Integrated Security=True;Async=True";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        PageAsyncTask pat = new PageAsyncTask(BeginAsync1, EndAsync1, null, null, true);
        this.RegisterAsyncTask(pat);
        pat = new PageAsyncTask(BeginAsync2, EndAsync2, null, null, true);
        this.RegisterAsyncTask(pat);
        pat = new PageAsyncTask(BeginAsync3, EndAsync3, null, null, false);
        this.RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync1(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("waitfor delay '00:00:01'", conn);
        IAsyncResult ar = cmd.BeginExecuteNonQuery(cb, cmd);
        return ar;
    }

    private void EndAsync1(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                int rows = cmd.EndExecuteNonQuery(ar);
            }
        }
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

    private IAsyncResult BeginAsync3(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("waitfor delay '00:00:01'", conn);
        IAsyncResult ar = cmd.BeginExecuteNonQuery(cb, cmd);
        return ar;
    }

    private void EndAsync3(IAsyncResult ar)
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
