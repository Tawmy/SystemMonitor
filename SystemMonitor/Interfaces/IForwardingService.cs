namespace SystemMonitor.Interfaces;

public interface IForwardingService
{
    Task<T> GetAsync<T>(string relativePath) where T : class;
    Task<T> GetAsync<T>(string relativePath, IDictionary<string, string> queryParameters) where T : class;
}