using System;
using System.Web;
using System.Web.SessionState;

namespace Samples
{
    public class ScalableSessionIDManager : SessionIDManager
    {
        public static string[] Machines = { "A", "B", "C" };
        private static Object randomLock = new Object();
        private static Random random = new Random();

        public override string CreateSessionID(HttpContext context)
        {
            int index;
            lock (randomLock)
            {
                index = random.Next(Machines.Length);
            }
            string id = Machines[index] + "." + base.CreateSessionID(context);
            return id;
        }

        public static string[] GetMachine(string id)
        {
            if (String.IsNullOrEmpty(id))
                return null;
            string[] values = id.Split('.');
            if (values.Length != 2)
                return null;
            for (int i = 0; i < Machines.Length; i++)
            {
                if (Machines[i] == values[0])
                    return values;
            }
            return null;
        }

        public override bool Validate(string id)
        {
            string[] values = GetMachine(id);
            return (values != null) && base.Validate(values[1]);
        }
    }
}