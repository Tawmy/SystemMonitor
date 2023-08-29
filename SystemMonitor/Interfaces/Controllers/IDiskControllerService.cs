using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IDiskControllerService
{
    MemoryMetrics GetDiskMetrics(string path);

    MemoryHealth GetDiskHeath(string path, decimal maximumPercentage);
}