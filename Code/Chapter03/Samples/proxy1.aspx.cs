using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class proxy1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TimeSpan age = TimeSpan.FromSeconds(60.0);
        this.Response.Cache.SetMaxAge(age);
        this.Response.Cache.SetCacheability(HttpCacheability.Public);
        this.Response.Cache.SetNoServerCaching();
    }
}
