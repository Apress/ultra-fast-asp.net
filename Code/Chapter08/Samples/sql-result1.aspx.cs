using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

public partial class sql_result1 : Page
{
    public const string ConnString = 
        "Data Source=server;Initial Catalog=Sample;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            int numRecords = Convert.ToInt32(this.cnt.Text);
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("[Traffic].[GetFirstLastPageViews]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameterCollection p = cmd.Parameters;
                    p.Add("count", SqlDbType.Int).Value = numRecords;
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        this.first.DataSource = reader;
                        this.first.DataBind();
                        reader.NextResult();
                        this.last.DataSource = reader;
                        this.last.DataBind();
                    }
                    catch (SqlException ex)
                    {
                        EventLog.WriteEntry("Application",
                            "Error in GetFirstLastPageView: " + ex.Message + "\n", 
                            EventLogEntryType.Error, 102);
                        throw;
                    }
                }
            }
        }
    }
}