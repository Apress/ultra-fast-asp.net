using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

public partial class sql_result2 : Page
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
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            DataSet results = new DataSet();
                            adapter.Fill(results);
                            this.first.DataSource = results.Tables[0];
                            this.first.DataBind();
                            this.last.DataSource = results.Tables[1];
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
}