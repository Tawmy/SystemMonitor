using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IMemoryService
{
    MemoryMetrics GetMemoryMetrics();

    MemoryHealth GetMemoryHealth(decimal maximumPercentage);
}