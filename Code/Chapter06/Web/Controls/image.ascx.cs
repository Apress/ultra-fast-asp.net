using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_image : UserControl
{
    private string _src;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.height <= 0 || this.width <= 0)
        {
            string path = Server.MapPath(this.src);
            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                using (System.Drawing.Image image =
                       System.Drawing.Image.FromStream(stream))
                {
                    width = image.Width;
                    height = image.Height;
                }
            }
        }
    }

    public string src
    {
        get
        {
            return this._src;
        }
        set
        {
            this._src = ResolveUrl(value).ToLowerInvariant();
        }
    }
    public int height { get; set; }
    public int width { get; set; }

    [Themeable(false)]
    public string alt { get; set; }
}


