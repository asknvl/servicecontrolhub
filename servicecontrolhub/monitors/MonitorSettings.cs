using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.monitors
{    
    public class MonitorSettings
    {
        #region properties
        public string service_name { get; set; } = "";
        public string service_url { get; set; } = "";
        public string auth_token { get; set; }
        public int diagnostics_period { get; set; } = 60;
        public int diagnostics_timeout { get; set; } = 60;
        #endregion
    }
}
