using System;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace Samples
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //
            // This requires enabling Service Broker:
            //      alter database [Database] set enable_broker with rollback immediate
            //
            string cs = ConfigurationManager.ConnectionStrings["data"].ConnectionString;
            //SqlDependency.Start(cs);
        }

        protected void Application_Stop(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["data"].ConnectionString;
            //SqlDependency.Stop(cs);
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "info")
            {
                HttpCookie cookie = context.Request.Cookies[arg];
                if (cookie != null)
                    return cookie.Value;
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}