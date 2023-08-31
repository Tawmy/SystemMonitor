using SystemMonitor.Records;

namespace SystemMonitor.Interfaces.Controllers;

public interface IMemoryControllerService
{
    Task<MemoryMetrics> GetMemoryMetrics(HttpRequest incomingRequest);

    Task<MemoryHealth> GetMemoryHealth(HttpRequest incomingRequest, decimal maximumPercentage);
}