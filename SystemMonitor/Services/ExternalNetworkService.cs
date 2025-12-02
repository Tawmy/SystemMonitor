using SystemMonitor.Interfaces;

namespace SystemMonitor.Services;

public class ExternalNetworkService(IForwardingService forwardingService) : INetworkService
{
    public Task<bool> VerifyTcpPortOpen(HttpRequest incomingRequest, string ipAddress, int port)
    {
        var parameters = new Dictionary<string, string> { { "ipAddress", ipAddress }, { "port", $"{port}" } };
        return forwardingService.GetAsync<bool>(incomingRequest, "Network/tcpPort", parameters);
    }
}