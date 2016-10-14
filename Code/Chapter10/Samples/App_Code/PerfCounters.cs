using System.Diagnostics;

public static class PerfCounters
{
    public static void Create()
    {
        if (!PerformanceCounterCategory.Exists("Membership"))
        {
            var ccd = new CounterCreationData("Logins", "Number of logins",
                PerformanceCounterType.NumberOfItems32);
            var ccdc = new CounterCreationDataCollection();
            ccdc.Add(ccd);
            ccd = new CounterCreationData("Ave Users", "Average number of users",
                PerformanceCounterType.AverageCount64);
            ccdc.Add(ccd);
            ccd = new CounterCreationData("Ave Users base", "Average number of users base",
                PerformanceCounterType.AverageBase);
            ccdc.Add(ccd);
            PerformanceCounterCategory.Create("Membership", "Website Membership system",
                PerformanceCounterCategoryType.MultiInstance, ccdc);
        }
    }

    public static void IncrementLogins()
    {
        var numLogins = new PerformanceCounter("Membership", "Logins", false);
        numLogins.Increment();
    }
}
