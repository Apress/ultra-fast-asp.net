using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view4 : System.Web.UI.Page
{
    public const string ViewKeyName = "__viewkey";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.viewlabel.Text = "not postback";
            this.ViewState["ok"] = "this is the ViewState text";
        }
        else
        {
            this.viewlabel.Text = (string)this.ViewState["ok"];
        }
    }

    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string key = Guid.NewGuid().ToString();
        this.ClientScript.RegisterHiddenField(ViewKeyName, key);
        this.Cache[key] = state;
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        string key = this.Request[ViewKeyName];
        if (key == null)
            throw new InvalidOperationException("Invalid ViewState Key");
        object state = this.Cache[key];
        if (state == null)
            throw new InvalidOperationException("ViewState too old");
        return state;
    }
}
