using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Records;

namespace SystemMonitor.Controllers;

[Route("[controller]")]
[ApiController]
public class MemoryController : ControllerBase
{
    private readonly IMemoryControllerService _controllerService;

    public MemoryController(IMemoryControllerService controllerService)
    {
        _controllerService = controllerService;
    }

    [HttpGet("metrics")]
    public Task<ActionResult<MemoryMetrics>> GetMemoryMetrics()
    {
        return Task.FromResult<ActionResult<MemoryMetrics>>(_controllerService.GetMemoryMetrics());
    }

    [HttpGet("health")]
    public Task<ActionResult<MemoryHealth>> GetMemoryHealth([Required] decimal maximumPercentage)
    {
        return Task.FromResult<ActionResult<MemoryHealth>>(_controllerService.GetMemoryHealth(maximumPercentage));
    }
}