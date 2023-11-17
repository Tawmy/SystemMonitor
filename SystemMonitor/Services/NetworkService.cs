using System.Net.Sockets;
using SystemMonitor.Interfaces;

namespace SystemMonitor.Services;

public class NetworkService : INetworkService
{
    public async Task<bool> VerifyTcpPortOpen(HttpRequest incomingRequest, string ipAddress, int port)
    {
        using var tcpClient = new TcpClient();

        try
        {
            await tcpClient.ConnectAsync(ipAddress, port);
            return true;
        }
        catch
        {
            return false;
        }
    }
}