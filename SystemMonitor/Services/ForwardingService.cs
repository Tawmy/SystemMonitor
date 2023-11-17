using System.Net.Http.Headers;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;

namespace SystemMonitor.Services;

public class ForwardingService : IForwardingService
{
    private readonly Uri _remoteServerUri =
        new(Environment.GetEnvironmentVariable(EnvironmentVariables.RemoteServerUri) ??
            throw new EnvironmentVariableNotSetException(EnvironmentVariables.RemoteServerUri));

    public async Task<T> GetAsync<T>(HttpRequest incomingRequest, string relativePath)
    {
        var httpClient = new HttpClient();

        var uri = new Uri(_remoteServerUri, relativePath);

        var bearer = incomingRequest.Headers.Authorization.FirstOrDefault();
        httpClient.DefaultRequestHeaders.Authorization = bearer != null
            ? AuthenticationHeaderValue.Parse(bearer)
            : null;

        HttpResponseMessage result;
        try
        {
            result = await httpClient.GetAsync(uri);
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

    public Task<T> GetAsync<T>(HttpRequest incomingRequest, string relativePath,
        IDictionary<string, string> queryParameters)
    {
        if (!queryParameters.Any())
        {
            return GetAsync<T>(incomingRequest, relativePath);
        }

        relativePath = $"{relativePath}{StitchQueryParameter(queryParameters.First(), true)}";

        foreach (var queryParameter in queryParameters.Skip(1))
        {
            relativePath = $"{relativePath}{StitchQueryParameter(queryParameter, false)}";
        }

        return GetAsync<T>(incomingRequest, relativePath);
    }

    private string StitchQueryParameter(KeyValuePair<string, string> queryParameter, bool firstParameter)
    {
        return $"{(firstParameter ? "?" : "&")}{queryParameter.Key}={queryParameter.Value}";
    }
}