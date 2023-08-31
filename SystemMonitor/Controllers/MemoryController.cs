using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Records;

namespace SystemMonitor.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class MemoryController : ControllerBase
{
    private readonly IMemoryControllerService _controllerService;

    public MemoryController(IMemoryControllerService controllerService)
    {
        _controllerService = controllerService;
    }

    [HttpGet("metrics")]
    public async Task<ActionResult<MemoryMetrics>> GetMemoryMetrics()
    {
        return await _controllerService.GetMemoryMetrics();
    }

    [HttpGet("health")]
    public async Task<ActionResult<MemoryHealth>> GetMemoryHealth([Required] decimal maximumPercentage)
    {
        return await _controllerService.GetMemoryHealth(maximumPercentage);
    }
}