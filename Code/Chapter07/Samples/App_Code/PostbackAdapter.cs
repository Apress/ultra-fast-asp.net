using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;

namespace Samples
{
    public class ActionHtmlWriter : HtmlTextWriter
    {
        public ActionHtmlWriter(TextWriter writer)
            : base(writer)
        {
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            if (name == "action")
            {
                HttpContext context = HttpContext.Current;
                value = context.Request.RawUrl;
            }
            base.WriteAttribute(name, value, fEncode);
        }
    }

    public class PostbackAdapter : ControlAdapter
    {
        public PostbackAdapter()
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ActionHtmlWriter actionWriter = new ActionHtmlWriter(writer);
            base.Render(actionWriter);
        }
    }
}
