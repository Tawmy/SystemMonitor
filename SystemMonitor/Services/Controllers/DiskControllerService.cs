using SystemMonitor.Interfaces;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Records;

namespace SystemMonitor.Services.Controllers;

public class DiskControllerService : IDiskControllerService
{
    private readonly IDiskService _diskService;

    public DiskControllerService(IDiskService diskService)
    {
        _diskService = diskService;
    }

    public Task<MemoryMetrics> GetDiskMetrics(HttpRequest incomingRequest, string path)
    {
        return _diskService.GetDiskMetricsAsync(incomingRequest, path);
    }

    public Task<MemoryHealth> GetDiskHeath(HttpRequest incomingRequest, string path, decimal maximumPercentage)
    {
        return _diskService.GetDiskHealthAsync(incomingRequest, path, maximumPercentage);
    }
}