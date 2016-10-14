using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cookie6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("name");
        cookie.Value = "value";
        cookie.HttpOnly = true;
        this.Response.AppendCookie(cookie);
    }
}
