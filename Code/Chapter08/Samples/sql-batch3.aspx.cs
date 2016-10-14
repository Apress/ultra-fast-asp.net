using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using System.Web.UI;

public partial class sql_batch3 : Page
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
            StringBuilder sb = new StringBuilder();
            string sql = "EXEC [Traffic].[AddPageView] @pvid{0} out, @userid{0}, @pvurl{0};";
            for (int i = 0; i < batchSize; i++)
            {
                sb.AppendFormat(sql, i);
            }
            string query = sb.ToString();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                conn.StatisticsEnabled = true;
                SqlParameterCollection p = null;
                for (int j = 0; j < numBatches; j++)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            conn.EnlistTransaction(Transaction.Current);
                            p = cmd.Parameters;
                            Guid userId = Guid.NewGuid();
                            for (int i = 0; i < batchSize; i++)
                            {
                                p.Add("pvid" + i, SqlDbType.BigInt).Direction = 
                                    ParameterDirection.Output;
                                p.Add("userid" + i, SqlDbType.UniqueIdentifier).Value = userId;
                                p.Add("pvurl" + i, SqlDbType.VarChar, 256).Value = 
                                    "http://www.12titans.net/test.aspx";
                            }
                            try
                            {
                                cmd.ExecuteNonQuery();
                                scope.Complete();
                            }
                            catch (SqlException ex)
                            {
                                EventLog.WriteEntry("Application",
                                    "Error in WritePageView: " + ex.Message + "\n", 
                                    EventLogEntryType.Error, 101);
                            }
                        }
                    }
                }
                StringBuilder result = new StringBuilder();
                result.Append("Last pvid = ");
                result.Append(p["pvid" + (batchSize - 1)].Value);
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
