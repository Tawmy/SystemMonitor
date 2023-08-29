using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IDiskService
{
    MemoryMetrics GetDiskMetrics(string path);

    MemoryHealth GetDiskHealth(string path, decimal maximumPercentage);
}