using System;
using System.Web.Caching;
using System.Web.UI;

public partial class cache1 : Page
{
    public static Object lockObject = new Object();

    protected void Page_Load(object sender, EventArgs e)
    {
        lock (lockObject)
        {
            if (this.Cache["key"] == null)
                this.Cache.Add("key", "value", null, DateTime.Now.AddSeconds(60),
                    Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }
    }
}
