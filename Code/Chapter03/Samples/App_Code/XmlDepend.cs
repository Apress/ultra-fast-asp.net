using System;
using System.Web;
using System.Web.Caching;
using System.Xml;

public class XmlDepend
{
    public static Object lockObject = new Object();

    public static XmlDocument MyDocument(string path)
    {
        string key = "mydoc:" + path;
        Cache cache = HttpContext.Current.Cache;
        lock (lockObject)
        {
            XmlDocument doc = (XmlDocument)cache[key];
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.Load(path);
                CacheDependency cd = new CacheDependency(path);
                cache.Insert(key, doc, cd);
            }
            return doc;
        }
    }
}
