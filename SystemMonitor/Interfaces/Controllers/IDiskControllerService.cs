using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IDiskControllerService
{
    Task<MemoryMetrics> GetDiskMetrics(string path);

    Task<MemoryHealth> GetDiskHeath(string path, decimal maximumPercentage);
}