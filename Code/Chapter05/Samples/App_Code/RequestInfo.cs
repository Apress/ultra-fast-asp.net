using System.Web;

namespace Samples
{
    public class RequestInfo
    {
        public string Page { get; set; }

        public RequestInfo()
        {
            this.Page = HttpContext.Current.Request.Url.ToString();
        }
    }
}

