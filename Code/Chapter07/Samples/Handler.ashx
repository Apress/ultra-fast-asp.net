<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;

public class Handler : IHttpAsyncHandler {
    public const string ConnString = 
        "Data Source=.;Initial Catalog=Sample;Integrated Security=True;Async=True";
    HttpContext Context { get; set; }

    public void ProcessRequest(HttpContext context)
    {
    }

    public IAsyncResult BeginProcessRequest(HttpContext context,
        AsyncCallback cb, object extraData)
    {
        this.Context = context;
        int fileid = 0;
        string id = context.Request.QueryString["id"];
        if (!String.IsNullOrEmpty(id))
            fileid = Convert.ToInt32(id);
        SqlConnection conn = new SqlConnection(ConnString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("GetHtml", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("fileId", SqlDbType.Int).Value = fileid;
        IAsyncResult ar = cmd.BeginExecuteReader(cb, cmd);
        return ar;
    }

    public void EndProcessRequest(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                SqlDataReader reader = cmd.EndExecuteReader(ar);
                while (reader.Read())
                {
                    Context.Response.Write(reader["Html"]);
                }
            }
        }
        Context.Response.ContentType = "text/html";
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}