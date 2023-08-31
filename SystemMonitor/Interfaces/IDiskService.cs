using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IDiskService
{
    Task<MemoryMetrics> GetDiskMetricsAsync(string path);

    Task<MemoryHealth> GetDiskHealthAsync(string path, decimal maximumPercentage);
}