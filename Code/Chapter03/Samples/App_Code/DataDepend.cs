using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Caching;

public static class DataDepend
{
    public static Object lockObject = new Object();
    public const string DataKey = "key";

    public static DataSet MyData()
    {
        DataSet ds;
        Cache cache = HttpContext.Current.Cache;
        lock (lockObject)
        {
            ds = (DataSet)cache[DataKey];
            if (ds == null)
            {
                string cs = ConfigurationManager.ConnectionStrings["data"]
                                                .ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string sql = "dbo.GetInfo";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            SqlCacheDependency dep = new SqlCacheDependency(cmd);
                            adapter.Fill(ds);
                            cache.Insert(DataKey, ds, dep);
                        }
                    }
                }
            }
        }
        return ds;
    }
}
