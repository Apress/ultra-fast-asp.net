using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class encrypt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("name");
        cookie.Value = Secure.EncryptToBase64("my secret text",
                            "password", this.Request.UserHostAddress);
        this.Response.AppendCookie(cookie);
    }
}
