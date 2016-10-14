using System;
using System.Web.UI;

public partial class session1 : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["test"] = "my data";
    }
}
