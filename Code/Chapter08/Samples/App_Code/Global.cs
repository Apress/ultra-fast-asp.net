using System;
using System.Threading;
using System.Web;

public class Global : HttpApplication
{
    private static Thread TaskThread { get; set; }

    public Global()
    {
    }

    void Application_Start(object sender, EventArgs e)
    {
        if ((TaskThread == null) || !TaskThread.IsAlive)
        {
            ThreadStart ts = new ThreadStart(BrokerWorker.Work);
            TaskThread = new Thread(ts);
            TaskThread.Start();
        }
        ConfigInfo.Start();
    }

    void Application_End(object sender, EventArgs e)
    {
        if ((TaskThread != null) && (TaskThread.IsAlive))
        {
            TaskThread.Abort();
        }
        TaskThread = null;
        ConfigInfo.Stop();
    }
}
