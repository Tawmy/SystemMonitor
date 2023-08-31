using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IMemoryControllerService
{
    Task<MemoryMetrics> GetMemoryMetrics();

    Task<MemoryHealth> GetMemoryHealth(decimal maximumPercentage);
}