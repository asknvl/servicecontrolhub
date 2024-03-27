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
    public class Protocol : IProtocol
    {
        #region vars
        ServiceCollection serviceCollection;
        IHttpClientFactory httpClientFactory;
        string url;
        string token;
        int timeout;
        #endregion

        public Protocol(string url, string token, int timeout)
        {
            this.url = url;
            this.token = token;
            this.timeout = timeout;

            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        }

        #region private
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
        }

        public async Task<serviceDiagnosticsDto> GetDiagnosticsResult()
        {            
            var addr = $"{url}/diagnostics";
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.Timeout = new TimeSpan(0, 0, timeout);

            var response = await httpClient.GetAsync(addr);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<serviceDiagnosticsDto>(result);            
        }
        #endregion

        #region public
        #endregion
    }
}
