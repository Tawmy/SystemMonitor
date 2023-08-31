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

    public Task<MemoryMetrics> GetMemoryMetrics()
    {
        return _memoryService.GetMemoryMetrics();
    }

    public Task<MemoryHealth> GetMemoryHealth(decimal maximumPercentage)
    {
        return _memoryService.GetMemoryHealth(maximumPercentage);
    }
}