using SystemMonitor.Interfaces;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Records;

namespace SystemMonitor.Services.Controllers;

public class MemoryControllerService : IMemoryControllerService
{
    private readonly IMemoryService _memoryService;

    public MemoryControllerService(IMemoryService memoryService)
    {
        _memoryService = memoryService;
    }

    public Task<MemoryMetrics> GetMemoryMetrics(HttpRequest incomingRequest)
    {
        return _memoryService.GetMemoryMetrics(incomingRequest);
    }

    public Task<MemoryHealth> GetMemoryHealth(HttpRequest incomingRequest, decimal maximumPercentage)
    {
        return _memoryService.GetMemoryHealth(incomingRequest, maximumPercentage);
    }
}