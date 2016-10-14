using System;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class file1 : Page
{
    private byte[] Data { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, null, null, true);
        RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        FileStream fs = new FileStream(this.Server.MapPath("csg.png"),
            FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
            FileOptions.Asynchronous | FileOptions.SequentialScan);
        this.Data = new byte[64 * 1024];
        IAsyncResult ar = fs.BeginRead(this.Data, 0, this.Data.Length, cb, fs);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        using (FileStream fs = (FileStream)ar.AsyncState)
        {
            int size = fs.EndRead(ar);
            this.LA.Text = "Size: " + size;
        }
    }
}
