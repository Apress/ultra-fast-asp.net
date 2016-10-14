using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class app1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpApplicationState app = this.Context.Application;
        string myValue = null;
        app.Lock();
        try
        {
            myValue = (string)app["key"];
            if (myValue == null)
            {
                myValue = "value";
                app["key"] = myValue;
            }
        }
        finally
        {
            app.UnLock();
        }
    }
}
