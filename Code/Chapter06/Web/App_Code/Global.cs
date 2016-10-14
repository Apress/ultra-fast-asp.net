using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Samples
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //
            // This only works in .NET 4.0
            //
            // HttpCapabilitiesBase.BrowserCapabilitiesProvider = new Samples.BrowserProvider();
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            switch (custom.ToLower())
            {
                case "iemozilla":
                    switch (context.Request.Browser.Browser.ToLower())
                    {
                        case "ie":
                        case "blazer 3.0":
                            return "ie";
                        case "mozilla":
                        case "firebird":
                        case "firefox":
                        case "applemac-safari":
                            return "mozilla";
                        default:
                            return "default";
                    }
                default:
                    return base.GetVaryByCustomString(context, custom);
            }
        }
    }
}
