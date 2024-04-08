using servicecontrolhub.config;
using servicecontrolhub.monitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.hub
{
    public class Hub : IHub
    {

        List<IMonitor> monitors = new List<IMonitor>();

        public Hub(Config config) { 

            foreach (var item in config.monitors) {
                var monitor = new monitors.Monitor(item, config.bot_settings);
                monitors.Add(monitor);
                monitor.Start();
            }


        }
    }
}
