namespace SystemMonitor.Interfaces;

public interface IForwardingService
{
    Task<T> GetAsync<T>(HttpRequest incomingRequest, string relativePath);

    Task<T> GetAsync<T>(HttpRequest incomingRequest, string relativePath, IDictionary<string, string> queryParameters);
}