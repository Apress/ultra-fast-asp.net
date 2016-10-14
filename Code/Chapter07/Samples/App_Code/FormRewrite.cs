using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Samples
{
    public class FormRewrite : HtmlForm
    {
        public FormRewrite()
        {
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            this.Action = this.Page.Request.RawUrl;
            base.RenderAttributes(writer);
        }
    }
}