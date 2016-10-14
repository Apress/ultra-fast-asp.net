using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class avoid_work : Page
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!this.IsPostBack)
        {
            //
            // Do expensive operations here that can be cached in ViewState,
            // cookies, etc.
            //
        }
        
        //
        // Check to see if the client is still connected before starting
        // long-running operations
        //
        if (this.Response.IsClientConnected)
        {
            // this.Server.Transfer("otherpage.aspx");
        }
    }

    protected virtual bool IsRefresh
    {
        get
        {
            return this.Request.Headers["Pragma"] == "no-cache" ||
                this.Request.Headers["Cache-Control"] == "max-age=0";
        }
    }
}
