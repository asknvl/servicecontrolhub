using Microsoft.Extensions.DependencyInjection;
using servicecontrolhub.monitors.protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace servicecontrolhub.monitors
{
    public class Monitor : IMonitor
    {
        #region vars                
        System.Timers.Timer timer = new System.Timers.Timer();

        IProtocol protocol;
        #endregion
        public Monitor(MonitorSettings settings) { 

            protocol = new Protocol(settings.service_url, settings.auth_token, settings.diagnostics_timeout);

            timer = new System.Timers.Timer();
            timer.Interval = settings.diagnostics_period * 1000;
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
        }

        #region private
        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var responce = await protocol.GetDiagnosticsResult();

            } catch (Exception ex)
            {

            }
        }
        #endregion

        #region public
        public void Start()
        {            
            timer.Start();
        }

        public void Stop()
        {         
            timer.Stop();
        }
        #endregion
    }
}
