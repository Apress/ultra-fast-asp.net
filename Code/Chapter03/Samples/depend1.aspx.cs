using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Caching;

public partial class depend1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["data"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(cs))
        {
            string sql = "dbo.GetInfo";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlCacheDependency dep = new SqlCacheDependency(cmd);
                mygrid.DataSource = cmd.ExecuteReader();
                mygrid.DataBind();
                this.Response.AddCacheDependency(dep);
            }
        }
    }
}
