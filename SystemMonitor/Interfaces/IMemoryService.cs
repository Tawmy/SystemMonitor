using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IMemoryService
{
    Task<MemoryMetrics> GetMemoryMetrics();

    Task<MemoryHealth> GetMemoryHealth(decimal maximumPercentage);
}