using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Samples
{
    public abstract class ADAL
    {
        public abstract IAsyncResult AddBrowserBegin(RequestInfo info, AsyncCallback callback);
        public abstract void AddBrowserEnd(IAsyncResult ar);

        public static string SampleDBConnectionString
        {
            get { return "Data Source=server;Initial Catalog=Sample;Integrated Security=True"; }
        }
    }
}
