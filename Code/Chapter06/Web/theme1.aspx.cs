using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class theme1 : Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.Theme = "mkt";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
