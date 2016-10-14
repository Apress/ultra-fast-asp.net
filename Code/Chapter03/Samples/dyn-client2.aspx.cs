using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dyn_client2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.Cache.SetExpires(DateTime.Now.AddDays(1.0));
        TimeSpan ds = new TimeSpan(1, 0, 0, 0);
        this.Response.Cache.SetMaxAge(ds);
    }
}
