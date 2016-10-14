using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

public partial class sql_batch1 : Page
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
                    table.Columns.Add("pvid", typeof(long));
                    table.Columns.Add("userid", typeof(Guid));
                    table.Columns.Add("pvurl", typeof(string));
                    using (SqlCommand cmd = new SqlCommand("[Traffic].[AddPageView]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameterCollection p = cmd.Parameters;
                        p.Add("@pvid", SqlDbType.BigInt, 0, "pvid").Direction = 
                            ParameterDirection.Output;
                        p.Add("@userid", SqlDbType.UniqueIdentifier, 0, "userid");
                        p.Add("@pvurl", SqlDbType.VarChar, 256, "pvurl");
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
                            adapter.InsertCommand = cmd;
                            adapter.UpdateBatchSize = batchSize;
                            Guid userId = Guid.NewGuid();
                            for (int i = 0; i < batchSize; i++)
                            {
                                table.Rows.Add(0, userId, "http://www.12titans.net/test.aspx");
                            }
                            try
                            {
                                adapter.Update(table);
                                pvid = (long)table.Rows[batchSize - 1]["pvid"];
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
