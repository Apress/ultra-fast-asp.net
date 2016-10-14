using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cookie5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("name");
        cookie.Value = "value";
        cookie.Domain = ".12titans.net";
        this.Response.AppendCookie(cookie);
    }
}
