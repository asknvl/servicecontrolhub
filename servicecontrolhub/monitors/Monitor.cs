using Microsoft.Extensions.DependencyInjection;
using servicecontrolhub.config;
using servicecontrolhub.logger;
using servicecontrolhub.monitors.protocol;
using servicecontrolhub.monitors.protocol.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace servicecontrolhub.monitors
{    
    public class Monitor : IMonitor
    {
        #region 
        const string TAG = "MNT";
        #endregion

        #region vars                
        System.Timers.Timer timer = new System.Timers.Timer();        
        IDiagnosticsRequestProtocol diagnosticsRx;
        IDiagnosticsTransmitProtocol diagnosticsToBotTx;
        ILogger logger;
        MonitorSettings monitor_settings;
        #endregion
        public Monitor(MonitorSettings monitor_settings, bot_notifier_settings bot_settings) {

            logger = new Logger("monitors", $"{monitor_settings.service_name}");
            this.monitor_settings = monitor_settings;

            diagnosticsRx = new DiagnosticsRequestProtocol(monitor_settings.service_url, monitor_settings.auth_token, monitor_settings.diagnostics_timeout);
            diagnosticsToBotTx = new MonitorToBotTransmitProtocol(bot_settings);

            timer = new System.Timers.Timer();
            timer.Interval = monitor_settings.diagnostics_period * 1000;
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
        }

        #region helpers
        string logErrorMessage(List<errorDto> erros)
        {

            if (erros == null)
                return string.Empty;

            string res = "";
            foreach (var err in erros)
            {
                logger.err(TAG, $"{err.entity} {err.description}");                
            }
            return res;
        }
        #endregion

        #region private
        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var responce = await diagnosticsRx.GetDiagnosticsResult();
                if (!responce.cheсk_result)
                {
                    logErrorMessage(responce.errors);
                    await diagnosticsToBotTx.SendDiagnosticsResult(responce);
                }

            } catch (Exception ex)
            {
                logger.err(TAG, $"\n{ex.Message}");
            }
        }
        #endregion

        #region public
        public void Start()
        {
            logger.inf(TAG, $"Monitor: {monitor_settings.service_name} started");
            timer.Start();
        }

        public void Stop()
        {
            logger.inf(TAG, $"Monitor: {monitor_settings.service_name} stopped");
            timer.Stop();
        }
        #endregion
    }
}
