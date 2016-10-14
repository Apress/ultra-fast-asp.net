using System.Diagnostics.CodeAnalysis;

namespace Samples
{
    public static class ConfigData
    {
        public static string TrafficConnectionStringAsync { get { return Async(TrafficConnectionString); } }
        public static string TrafficConnectionString { get; set; }

        private static string Async(string connString)
        {
            return connString + ";async=true";
        }
    }
}
