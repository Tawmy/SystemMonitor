using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IMemoryControllerService
{
    MemoryMetrics GetMemoryMetrics();

    MemoryHealth GetMemoryHealth(decimal maximumPercentage);
}