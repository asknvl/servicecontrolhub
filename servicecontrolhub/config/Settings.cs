using servicecontrolhub.monitors;
using servicecontrolhub.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.config
{
    public class Settings
    {

        #region properties
        public Config config { get; set; } = new();
        #endregion

        #region singletone
        private static Settings instance;
        private Settings() {
            var storage = new Storage<Config>("config", config);
            config = storage.load();
        }

        public static Settings getInstance()
        {
            if (instance == null)
                instance = new Settings();  
            return instance;
        }
        #endregion

    }

    public class keepalive_settings  
    {
        public int port { get; set; } = 5050;
    }

    public class bot_notifier_settings
    {
        public string url { get; set; } = "";
        public string auth_token { get; set; } = "";
    }

    public class Config
    {
        public keepalive_settings keepalive_settings { get; set; } = new();
        public bot_notifier_settings bot_settings { get; set; } = new();
        public List<MonitorSettings> monitors { get; set; } = new();

        public Config()
        {
        }
    }

}
