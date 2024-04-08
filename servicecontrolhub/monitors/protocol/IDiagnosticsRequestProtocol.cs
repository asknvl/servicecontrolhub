using servicecontrolhub.monitors.protocol.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.monitors.protocol
{
    public interface IDiagnosticsRequestProtocol
    {
        Task<serviceDiagnosticsDto> GetDiagnosticsResult();
    }
}
