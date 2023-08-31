using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Interfaces.Controllers;
using SystemMonitor.Records;

namespace SystemMonitor.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class DiskController : ControllerBase
{
    private readonly IDiskControllerService _controllerService;

    public DiskController(IDiskControllerService controllerService)
    {
        _controllerService = controllerService;
    }

    [HttpGet("metrics")]
    public async Task<ActionResult<MemoryMetrics>> GetMemoryMetrics([Required] string path)
    {
        return await _controllerService.GetDiskMetrics(Request, path);
    }

    [HttpGet("health")]
    public async Task<ActionResult<MemoryHealth>> GetMemoryHealth([Required] string path,
        [Required] decimal maximumPercentage)
    {
        return await _controllerService.GetDiskHeath(Request, path, maximumPercentage);
    }
}