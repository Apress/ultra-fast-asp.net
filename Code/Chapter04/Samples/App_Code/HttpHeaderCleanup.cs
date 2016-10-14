using System;
using System.Web;

namespace Samples
{
    public class HttpHeaderCleanup : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Headers.Remove("Server");
            response.Headers.Remove("ETag");
        }

        public void Dispose()
        {
        }
    }
}
