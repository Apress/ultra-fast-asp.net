using System;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web;

//
// Note: the issue that this code is intended to address, as described
// on page 246, seems to have been fixed in a recent patch to .NET 3.5;
// however, the technique is still useful in other scenarios.
//
namespace Samples
{
    public class RewritePageAdapter : PageAdapter
    {
        public RewritePageAdapter()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            context.RewritePath(this.Page.Request.RawUrl);
            base.OnInit(e);
        }
    }
}