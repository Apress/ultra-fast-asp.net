using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cookie8 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = this.Request.Cookies["name"];
        if (cookie != null)
        {
            string value = cookie.Value;
        }
    }
}
