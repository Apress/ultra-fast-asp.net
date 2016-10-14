using System;
using System.Text;
using System.Web;
using System.Globalization;

namespace Samples
{
    public class RequestInfo
    {
        public const string REQ_INFO = "info";

        public RequestInfo(Guid machineId, bool first, bool firstResponse)
        {
            this.MachineId = machineId;
            this.First = first;
            this.FirstResponse = firstResponse;

            //
            // Store Context info here so that it's available in worker threads
            //
            this.Page = HttpContext.Current.Request.Url.ToString();
        }

        public static RequestInfo Create(Guid machineId, bool first, bool firstResponse)
        {
            RequestInfo info = new RequestInfo(machineId, first, firstResponse);
            HttpContext.Current.Items[REQ_INFO] = info;
            return info;
        }

        public static RequestInfo Get()
        {
            RequestInfo info = (RequestInfo)HttpContext.Current.Items[REQ_INFO];
            return info;
        }

        public Guid MachineId { get; set; }
        public bool First { get; set; }
        public bool FirstResponse { get; set; }
        public string Page { get; set; }
    }
}

