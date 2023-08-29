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
    public ActionResult<MemoryMetrics> GetMemoryMetrics([Required] string path)
    {
        return _controllerService.GetDiskMetrics(path);
    }

    [HttpGet("health")]
    public ActionResult<MemoryHealth> GetMemoryHealth([Required] string path, [Required] decimal maximumPercentage)
    {
        return _controllerService.GetDiskHeath(path, maximumPercentage);
    }
}