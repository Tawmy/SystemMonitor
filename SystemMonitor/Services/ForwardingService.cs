using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;

namespace SystemMonitor.Services;

public class ForwardingService : IForwardingService
{
    private readonly Uri _remoteServerUri =
        new(Environment.GetEnvironmentVariable(EnvironmentVariables.RemoteServerUri) ??
            throw new EnvironmentVariableNotSetException(EnvironmentVariables.RemoteServerUri));

    private static HttpClient HttpClient => new();

    public async Task<T> GetAsync<T>(string relativePath) where T : class
    {
        var uri = new Uri(_remoteServerUri, relativePath);

        HttpResponseMessage result;
        try
        {
            result = await HttpClient.GetAsync(uri);
        }
        catch (Exception e)
        {
            throw new RemoteMonitorErrorException($"GET {uri}", e);
        }

        var metrics = await result.Content.ReadFromJsonAsync<T>();

        if (metrics == null)
        {
            throw new RemoteMonitorErrorException(nameof(metrics));
        }

        return metrics;
    }

    public Task<T> GetAsync<T>(string relativePath, IDictionary<string, string> queryParameters) where T : class
    {
        if (!queryParameters.Any())
        {
            return GetAsync<T>(relativePath);
        }

        relativePath = $"{relativePath}{StitchQueryParameter(queryParameters.First(), true)}";

        foreach (var queryParameter in queryParameters.Skip(1))
        {
            relativePath = $"{relativePath}{StitchQueryParameter(queryParameter, false)}";
        }

        return GetAsync<T>(relativePath);
    }

    private string StitchQueryParameter(KeyValuePair<string, string> queryParameter, bool firstParameter)
    {
        return $"{(firstParameter ? "?" : "&")}{queryParameter.Key}={queryParameter.Value}";
    }
}