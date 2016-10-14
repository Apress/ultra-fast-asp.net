using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

public partial class sql_batch5 : Page
{
    public const string ConnString = 
        "Data Source=server;Initial Catalog=Sample;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            int numRecords = Convert.ToInt32(this.cnt.Text);
            int batchSize = Convert.ToInt32(this.sz.Text);
            int numBatches = numRecords / batchSize;
            long pvid = -1;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                conn.StatisticsEnabled = true;
                for (int j = 0; j < numBatches; j++)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("userid", typeof(Guid));
                    table.Columns.Add("pvurl", typeof(string));
                    using (SqlCommand cmd = new SqlCommand("[Traffic].[AddPageViewTVP]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        Guid userId = Guid.NewGuid();
                        for (int i = 0; i < batchSize; i++)
                        {
                            table.Rows.Add(userId, "http://www.12titans.net/test.aspx");
                        }
                        SqlParameterCollection p = cmd.Parameters;
                        p.Add("pvid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        SqlParameter rt = p.AddWithValue("rows", table);
                        rt.SqlDbType = SqlDbType.Structured;
                        rt.TypeName = "PageViewType";
                        try
                        {
                            cmd.ExecuteNonQuery();
                            pvid = (long)p["pvid"].Value;
                        }
                        catch (SqlException ex)
                        {
                            EventLog.WriteEntry("Application",
                                "Error in WritePageView: " + ex.Message + "\n", 
                                EventLogEntryType.Error, 101);
                            break;
                        }
                    }
                }
                StringBuilder result = new StringBuilder();
                result.Append("Last pvid = ");
                result.Append(pvid.ToString());
                result.Append("<br/>");
                IDictionary dict = conn.RetrieveStatistics();
                foreach (string key in dict.Keys)
                {
                    result.Append(key);
                    result.Append(" = ");
                    result.Append(dict[key]);
                    result.Append("<br/>");
                }
                this.info.Text = result.ToString();
            }
        }
    }
}
