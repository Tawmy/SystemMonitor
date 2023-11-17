namespace SystemMonitor.Interfaces;

public interface INetworkService
{
    Task<bool> VerifyTcpPortOpen(HttpRequest incomingRequest, string ipAddress, int port);
}