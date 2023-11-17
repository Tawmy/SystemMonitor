using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Interfaces.Controllers;

namespace SystemMonitor.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class NetworkController(INetworkControllerService controllerService) : ControllerBase
{
    [HttpGet("tcpPort")]
    public async Task<ActionResult<bool>> VerifyTcpPortOpen([Required] string ipAddress, [Required] int port)
    {
        return await controllerService.VerifyTcpPortOpen(Request, ipAddress, port);
    }
}