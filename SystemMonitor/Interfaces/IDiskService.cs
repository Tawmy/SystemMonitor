using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IDiskService
{
    Task<MemoryMetrics> GetDiskMetricsAsync(HttpRequest incomingRequest, string path);

    Task<MemoryHealth> GetDiskHealthAsync(HttpRequest incomingRequest, string path, decimal maximumPercentage);
}