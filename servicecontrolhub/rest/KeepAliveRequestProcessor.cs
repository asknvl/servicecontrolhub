using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.rest
{
    public class KeepAliveRequestProcessor : IRequestProcessor
    {

        #region var
        int cntr = 0;
        #endregion

        public async Task<(HttpStatusCode, string)> ProcessRequest()
        {
            await Task.CompletedTask;
            return (HttpStatusCode.OK, $"{cntr++}");
        }

        public Task<(HttpStatusCode, string)> ProcessRequestData(string data)
        {
            throw new NotImplementedException();
        }
    }
}
