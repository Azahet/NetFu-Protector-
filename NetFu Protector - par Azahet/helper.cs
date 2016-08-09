using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Drawing;

namespace NetFu_Protector___par_Azahet
{
    class helper
    {
        public static string get_proces()
        {
            Process[] processlist = Process.GetProcesses();
            var t = string.Empty;
            foreach (Process theprocess in processlist)
            {
                if (t != "NetFu Protector - par Azahet")
                {
                    t += theprocess.ProcessName + Environment.NewLine;
                }
            }
           return t;
        }
    }
}
