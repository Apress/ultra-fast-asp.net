using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class async_timeout : Page
{
    public const string ConnString = "Data Source=.;Integrated Security=True;Async=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.AsyncTimeout = TimeSpan.FromSeconds(5);
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, TimeoutAsync, null, true);
        RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("waitfor delay '00:01:00'", conn);
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

    private void TimeoutAsync(IAsyncResult ar)
    {
        errorLabel.Text = "Database timeout error.";
        SqlCommand cmd = (SqlCommand)ar.AsyncState;
        cmd.Connection.Dispose();
        cmd.Dispose();
    }
}
