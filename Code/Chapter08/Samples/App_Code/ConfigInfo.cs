using System.Data;
using System.Data.SqlClient;

public static class ConfigInfo
{
    public const string ConnString = 
        "Data Source=.;Initial Catalog=Sample;Integrated Security=True";
    public static DataTable ConfigTable { get; set; }

    public static void Start()
    {
        SqlDependency.Start(ConnString);
        LoadConfig();
    }

    public static void Stop()
    {
        SqlDependency.Stop(ConnString);
    }

    private static void LoadConfig()
    {
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetConfigInfo]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDependency depend = new SqlDependency(cmd);
                depend.OnChange += OnConfigChange;
                ConfigTable = new DataTable();
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ConfigTable.Load(reader);
                }
            }
        }
    }

    private static void OnConfigChange(object sender, SqlNotificationEventArgs e)
    {
        SqlDependency depend = (SqlDependency)sender;
        depend.OnChange -= OnConfigChange;
        if (e.Type == SqlNotificationType.Change)
            LoadConfig();
    }
}
