using System;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web;

namespace Samples
{
    public class MobilePersister : PageStatePersister
    {
        public const string ViewKeyName = "__viewkey";
        private const string _viewCache = "view";
        private const string _controlCache = "ctl";

        public MobilePersister(Page page)
            : base(page)
        {
        }

        public override void Save()
        {
            if ((this.ViewState != null) || (this.ControlState != null))
            {
                string key = Guid.NewGuid().ToString();
                this.Page.RegisterHiddenField(ViewKeyName, key);
                if (this.ViewState != null)
                    this.Page.Cache[key + _viewCache] = this.ViewState;
                if (this.ControlState != null)
                    this.Page.Cache[key + _controlCache] = this.ControlState;
            }
        }

        public override void Load()
        {
            string key = this.Page.Request[ViewKeyName];
            if (key != null)
            {
                this.ViewState = this.Page.Cache[key + _viewCache];
                this.ControlState = this.Page.Cache[key + _controlCache];
            }
        }
    }

    public class MyPageAdapter : PageAdapter
    {
        public MyPageAdapter()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            context.RewritePath(this.Page.Request.RawUrl);
            base.OnInit(e);
        }

        public override PageStatePersister GetStatePersister()
        {
            if (this.Page.Request.Browser.IsMobileDevice)
            {
                return new MobilePersister(this.Page);
            }
            else
                return base.GetStatePersister();
        }
    }
}
