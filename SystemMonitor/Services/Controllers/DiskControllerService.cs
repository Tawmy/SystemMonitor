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

    public MemoryMetrics GetDiskMetrics(string path)
    {
        return _diskService.GetDiskMetrics(path);
    }

    public MemoryHealth GetDiskHeath(string path, decimal maximumPercentage)
    {
        return _diskService.GetDiskHealth(path, maximumPercentage);
    }
}