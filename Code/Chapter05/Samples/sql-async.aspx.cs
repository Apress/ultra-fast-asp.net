using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class sql_async : Page
{
    public const string ConnString = "Data Source=.;Integrated Security=True;Async=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, null, null, true);
        this.RegisterAsyncTask(pat);
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
    }
}
