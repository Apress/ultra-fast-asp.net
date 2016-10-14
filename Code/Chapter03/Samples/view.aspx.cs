using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view : Page
{
    private const string SortStateKey = "SO";
    private const string SortAscending = "a";
    public bool IsSortAscending { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string prevSort = (string)this.ViewState[SortStateKey];
            this.IsSortAscending = prevSort == SortAscending;
        }
        else
        {
            this.ViewState[SortStateKey] = SortAscending;
            this.IsSortAscending = true;
        }
    }
}
