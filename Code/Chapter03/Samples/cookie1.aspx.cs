using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cookie1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("name");
        cookie.Value = "value";
        this.Response.AppendCookie(cookie);
    }
}
