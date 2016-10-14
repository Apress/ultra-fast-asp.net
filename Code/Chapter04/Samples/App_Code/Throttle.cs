using System;
using System.Web;

namespace Samples
{
    public class Throttle : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostRequestHandlerExecute += OnPostRequestHandlerExecute;
        }

        void OnPostRequestHandlerExecute(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;
            if (response.ContentType == "application/x-zip-compressed")
            {
                HttpRequest request = context.Request;
                if (!String.IsNullOrEmpty(request.ServerVariables["SERVER_SOFTWARE"]))
                {
                    request.ServerVariables["ResponseThrottler-InitialSendSize"] = "20";
                    request.ServerVariables["ResponseThrottler-Rate"] = "10";
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
