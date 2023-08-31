using System.Globalization;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class ExternalDiskService : IDiskService
{
    private readonly IForwardingService _forwardingService;

    public ExternalDiskService(IForwardingService forwardingService)
    {
        _forwardingService = forwardingService;
    }

    public async Task<MemoryMetrics> GetDiskMetricsAsync(HttpRequest incomingRequest, string path)
    {
        var parameters = new Dictionary<string, string> {{"path", path}};
        return await _forwardingService.GetAsync<MemoryMetrics>(incomingRequest, "Disk/Metrics", parameters);
    }

    public async Task<MemoryHealth> GetDiskHealthAsync(HttpRequest incomingRequest, string path,
        decimal maximumPercentage)
    {
        var parameters = new Dictionary<string, string>
        {
            {"path", path},
            {"maximumPercentage", maximumPercentage.ToString(CultureInfo.InvariantCulture)}
        };
        return await _forwardingService.GetAsync<MemoryHealth>(incomingRequest, "Disk/Health", parameters);
    }
}