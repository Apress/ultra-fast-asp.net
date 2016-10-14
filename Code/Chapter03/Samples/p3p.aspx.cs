using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class p3p : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.AddHeader("P3P", "CP=\"NID DSP CAO COR\"");
    }
}
