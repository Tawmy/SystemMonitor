using SystemMonitor.Interfaces;
using SystemMonitor.Interfaces.Controllers;

namespace SystemMonitor.Services.Controllers;

public class NetworkControllerService(INetworkService networkService) : INetworkControllerService
{
    public Task<bool> VerifyTcpPortOpen(HttpRequest incomingRequest, string ipAddress, int port)
    {
        return networkService.VerifyTcpPortOpen(incomingRequest, ipAddress, port);
    }
}