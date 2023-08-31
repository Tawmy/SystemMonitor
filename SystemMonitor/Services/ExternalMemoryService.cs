using System.Globalization;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class ExternalMemoryService : IMemoryService
{
    private readonly IForwardingService _forwardingService;

    public ExternalMemoryService(IForwardingService forwardingService)
    {
        _forwardingService = forwardingService;
    }

    public Task<MemoryMetrics> GetMemoryMetrics()
    {
        return _forwardingService.GetAsync<MemoryMetrics>("Memory/metrics");
    }

    public Task<MemoryHealth> GetMemoryHealth(decimal maximumPercentage)
    {
        var parameters = new Dictionary<string, string>
            {{"maximumPercentage", maximumPercentage.ToString(CultureInfo.InvariantCulture)}};
        return _forwardingService.GetAsync<MemoryHealth>("Memory/health", parameters);
    }
}