using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class compress : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.Request.ServerVariables["SERVER_SOFTWARE"]))
            this.Request.ServerVariables["IIS_EnableDynamicCompression"] = "1";
    }
}
