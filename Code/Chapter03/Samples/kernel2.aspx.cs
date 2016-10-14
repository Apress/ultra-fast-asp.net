using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class kernel2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TimeSpan age = TimeSpan.FromDays(1.0);
        this.Response.Cache.SetMaxAge(age);
        this.Response.Cache.SetCacheability(HttpCacheability.Public);
    }
}
