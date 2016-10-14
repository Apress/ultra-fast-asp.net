using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class server2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TimeSpan expires = TimeSpan.FromSeconds(300.0);
        this.Response.Cache.SetMaxAge(expires);
        this.Response.Cache.SetVaryByCustom("info");
        this.Response.Cache.SetCacheability(HttpCacheability.Server);
        this.Response.Cache.SetValidUntilExpires(true);
    }
}
