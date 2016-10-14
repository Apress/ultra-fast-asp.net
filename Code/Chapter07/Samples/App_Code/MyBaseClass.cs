using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public class MyBaseClass : Page
{
    public MyBaseClass()
    {
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        return base.LoadPageStateFromPersistenceMedium();
    }

    protected override void SavePageStateToPersistenceMedium(object state)
    {
        base.SavePageStateToPersistenceMedium(state);
    }
}