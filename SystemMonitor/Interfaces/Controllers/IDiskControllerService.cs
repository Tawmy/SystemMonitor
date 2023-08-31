using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IDiskControllerService
{
    Task<MemoryMetrics> GetDiskMetrics(HttpRequest incomingRequest, string path);

    Task<MemoryHealth> GetDiskHeath(HttpRequest incomingRequest, string path, decimal maximumPercentage);
}