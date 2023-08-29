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
    public ActionResult<MemoryMetrics> GetMemoryMetrics()
    {
        return _controllerService.GetMemoryMetrics();
    }

    [HttpGet("health")]
    public ActionResult<MemoryHealth> GetMemoryHealth([Required] decimal maximumPercentage)
    {
        return _controllerService.GetMemoryHealth(maximumPercentage);
    }
}