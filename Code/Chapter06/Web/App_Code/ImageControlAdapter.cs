using System;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;

namespace Samples
{
    public class ImageControlAdapter : WebControlAdapter
    {
        public ImageControlAdapter()
        {
        }

        protected override void BeginRender(System.Web.UI.HtmlTextWriter writer)
        {
            Image image = Control as Image;
            if ((image != null) && !String.IsNullOrEmpty(image.ImageUrl))
            {
                if (!image.ImageUrl.StartsWith("http"))
                {
                    image.ImageUrl = this.Page.ResolveUrl(image.ImageUrl).ToLower();
                }
            }
            base.BeginRender(writer);
        }
    }
}
