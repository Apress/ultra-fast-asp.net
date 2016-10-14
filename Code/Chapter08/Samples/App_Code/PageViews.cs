using System.Data;
using System.Threading;
using System.Web;

namespace Samples
{
    public class PageViews
    {
        public PageViews()
        {
            ManualResetEvent dataReady = 
                (ManualResetEvent)HttpContext.Current.Items["dataready"];
            dataReady.WaitOne();
        }

        public int GetCount()
        {
            int count = (int)HttpContext.Current.Items["count"];
            return count;
        }

        public DataTable GetRows(int startRowIndex, int maximumRows)
        {
            DataTable data = (DataTable)HttpContext.Current.Items["data"];
            return data;
        }
    }
}