using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using servicecontrolhub.monitors.protocol.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.monitors.protocol
{
    public class DiagnosticsRequestProtocol : IDiagnosticsRequestProtocol
    {
        #region vars
        ServiceCollection serviceCollection;
        IHttpClientFactory httpClientFactory;
        HttpClient httpClient;
        string url;        
        #endregion

        public DiagnosticsRequestProtocol(string url, string token, int timeout)
        {
            this.url = url;

            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClient = httpClientFactory.CreateClient();

            httpClient.Timeout = new TimeSpan(0, 0, timeout);
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #region private
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
        }

        public async Task<serviceDiagnosticsDto> GetDiagnosticsResult()
        {
            string sres = string.Empty;

            var addr = $"{url}/diagnostics";                                    
            var response = await httpClient.GetAsync(addr);

            try
            {
                response.EnsureSuccessStatusCode();
                sres = await response.Content.ReadAsStringAsync();
            } catch (Exception ex)
            {
                throw new Exception($"GetDiagnosticsResult {ex.Message}");
            }
            return JsonConvert.DeserializeObject<serviceDiagnosticsDto>(sres);            
        }
        #endregion

        #region public
        #endregion
    }
}
