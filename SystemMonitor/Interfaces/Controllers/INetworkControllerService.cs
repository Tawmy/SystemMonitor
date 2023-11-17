namespace SystemMonitor.Interfaces.Controllers;

public interface INetworkControllerService
{
    Task<bool> VerifyTcpPortOpen(HttpRequest incomingRequest, string ipAddress, int port);
}