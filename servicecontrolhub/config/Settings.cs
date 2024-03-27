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

    public class Rest  
    {
        public int port { get; set; } = 5050;
    }

    public class Output
    {
        public string url { get; set; } = "";
    }

    public class Config
    {
        public Rest rest { get; set; } = new();
        public Output output { get; set; } = new();
        public List<MonitorSettings> monitors { get; set; } = new();

        public Config()
        {
            monitors.Add(new MonitorSettings());
        }
    }

}
