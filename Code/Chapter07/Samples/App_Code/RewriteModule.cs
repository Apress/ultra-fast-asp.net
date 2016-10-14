using System;
using System.Web;

namespace Samples
{
    public class RewriteModule : IHttpModule
    {
        public RewriteModule()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Sample_BeginRequest;
        }

        private void Sample_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string path = context.Request.RawUrl;
            if (path.Contains("/p/"))
            {
                string newUrl = path.Replace("/p/", "/mycoolproductpages/") + "?x=y";
                context.RewritePath(newUrl, false);
            }
        }

        public void Dispose()
        {
        }
    }
}
