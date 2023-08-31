using SystemMonitor.Records;

namespace SystemMonitor.Interfaces;

public interface IMemoryService
{
    Task<MemoryMetrics> GetMemoryMetrics(HttpRequest incomingRequest);

    Task<MemoryHealth> GetMemoryHealth(HttpRequest incomingRequest, decimal maximumPercentage);
}