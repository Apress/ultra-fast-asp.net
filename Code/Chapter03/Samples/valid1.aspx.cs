using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class valid1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCacheValidateHandler val = new HttpCacheValidateHandler(ValidateCache);
        this.Response.Cache.AddValidationCallback(val, null);
    }

    public static void ValidateCache(HttpContext context, Object data,
                                     ref HttpValidationStatus status)
    {
        HttpCookie cookie = context.Request.Cookies["admin"];
        if (cookie != null)
            status = HttpValidationStatus.Invalid;
        else
            status = HttpValidationStatus.Valid;
    }
}
