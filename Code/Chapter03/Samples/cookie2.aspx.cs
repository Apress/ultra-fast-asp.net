using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cookie2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("name");
        cookie.Values["v1"] = "value1";
        cookie.Values["v2"] = "value2";
        this.Response.AppendCookie(cookie);
    }
}
