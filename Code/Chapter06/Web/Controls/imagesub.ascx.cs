using System;
using System.Web;
using System.Web.UI;

public partial class Controls_imagesub : UserControl
{
    private string _src;
    private static string[] subdomains = { 
        "http://s1.12titans.net",
        "http://s2.12titans.net",
        "http://s3.12titans.net"
    };

    public string src
    {
        get
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Request.Url.Host != "localhost")
            {
                int n = Math.Abs(this._src.GetHashCode()) % subdomains.Length;
                return subdomains[n] + this._src;
            }
            else
            {
                return this._src;
            }
        }
        set
        {
            this._src = ResolveUrl(value).ToLowerInvariant();
        }
    }
    public int height { get; set; }
    public int width { get; set; }
    [Themeable(false)]
    public string alt { get; set; }
}
