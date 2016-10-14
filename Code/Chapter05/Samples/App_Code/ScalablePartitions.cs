using System.Web;

namespace Samples
{
    public class ScalablePartitions : IPartitionResolver
    {
        private string[] sessionServers = {
            "Data Source=.;Initial Catalog=ASPState;Integrated Security=True",
            "Data Source=.;Initial Catalog=ASPState;Integrated Security=True",
            "Data Source=.;Initial Catalog=ASPState;Integrated Security=True"
        };

        public void Initialize()
        {
        }

        public string ResolvePartition(object key)
        {
            string id = (string)key;
            string[] values = ScalableSessionIDManager.GetMachine(id);
            string cs = null;
            if (values != null)
            {
                for (int i = 0; i < ScalableSessionIDManager.Machines.Length; i++)
                {
                    if (values[0] == ScalableSessionIDManager.Machines[i])
                    {
                        cs = sessionServers[i];
                        break;
                    }
                }
            }
            return cs;
        }
    }
}
