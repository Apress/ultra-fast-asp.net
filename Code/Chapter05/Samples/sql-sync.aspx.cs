using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class sql_sync : Page
{
    public const string ConnString = "Data Source=.;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("waitfor delay '00:00:01'", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
