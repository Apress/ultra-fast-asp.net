using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class webreq1 : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AsyncTimeout = TimeSpan.FromSeconds(30);
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, TimeoutAsync, null, true);
        RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        WebRequest request = WebRequest.Create("http://www.apress.com/");
        IAsyncResult ar = request.BeginGetResponse(cb, request);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        WebRequest request = (WebRequest)ar.AsyncState;
        WebResponse response = request.EndGetResponse(ar);
        StringBuilder sb = new StringBuilder();
        foreach (string header in response.Headers.Keys)
        {
            sb.Append(header);
            sb.Append(": ");
            sb.Append(response.Headers[header]);
            sb.Append("<br/>");
        }
        this.LO.Text = sb.ToString();
    }

    private void TimeoutAsync(IAsyncResult ar)
    {
        this.LO.Text = "Web request timed out.";
    }
}
