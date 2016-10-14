using System;
using System.Threading;
using System.Web.UI;

public partial class flush1 : Page
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        this.Response.Write("<!DOCTYPE html PUBLIC " +
            "\"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
            "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
        this.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
        HtmlTextWriter writer = this.CreateHtmlTextWriter(this.Response.Output);
        this.Header.RenderControl(writer);
        writer.Flush();
        this.Response.Flush();
        this.Controls.Remove(this.Header);
        Thread.Sleep(2000);
    }
}
