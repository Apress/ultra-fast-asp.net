using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;

//
// This class only works with .NET 4.0
//
namespace Samples
{
#if NET4_0
    public class BrowserProvider : HttpCapabilitiesProvider
    {
        public BrowserProvider()
        {
        }

        public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
        {
            string key = "bw-" + request.UserAgent;
            Cache cache = HttpContext.Current.Cache;
            HttpBrowserCapabilities caps = cache[key] as HttpBrowserCapabilities;
            if (caps == null)
            {
                //
                // Determine browser type here...
                //
                caps = new HttpBrowserCapabilities();
                caps.AddBrowser("test");
                Hashtable capDict = new Hashtable(StringComparer.OrdinalIgnoreCase);
                capDict["browser"] = "Default";
                capDict["cookies"] = "true";
                capDict["ecmascriptversion"] = "0.0";
                capDict["tables"] = "true";
                capDict["w3cdomversion"] = "0.0";
                caps.Capabilities = capDict;
                cache.Insert(key, caps, null, Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(60.0));
            }
            return caps;
        }
    }
#endif
}