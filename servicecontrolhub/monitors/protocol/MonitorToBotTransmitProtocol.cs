using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using servicecontrolhub.config;
using servicecontrolhub.monitors.protocol.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace servicecontrolhub.monitors.protocol
{
    public class MonitorToBotTransmitProtocol : IDiagnosticsTransmitProtocol
    {

        #region vars
        ServiceCollection serviceCollection;
        IHttpClientFactory httpClientFactory;
        HttpClient httpClient;
        bot_notifier_settings settings;        
        #endregion

        public MonitorToBotTransmitProtocol(bot_notifier_settings settings) { 
        
            this.settings = settings;
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClient = httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(settings.auth_token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.auth_token);
        }

        #region private
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
        }
        #endregion

        public async Task SendDiagnosticsResult(serviceDiagnosticsDto dres)
        {
            var addr = $"{settings.url}/diagnostics";
            var json = JsonConvert.SerializeObject(dres);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(addr, data);
                response.EnsureSuccessStatusCode();
            } catch (Exception ex)
            {
                throw new Exception($"SendDiagnosticsResult {ex.Message}");
            }            
        }
    }
}
