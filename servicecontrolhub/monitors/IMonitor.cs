using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.monitors
{
    public interface IMonitor
    {
        public void Start();
        public void Stop();
    }
}
