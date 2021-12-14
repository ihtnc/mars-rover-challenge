using Microsoft.AspNetCore.Mvc;
using MarsRoverChallenge.Apps.Web.Models;
using MarsRoverChallenge.Apps.Web.Services;

namespace MarsRoverChallenge.Apps.Web.Controllers;

[ApiController]
[Route("api")]
public class RoverCmdController : ControllerBase
{
    public readonly IRoverCommandService _service;

    public RoverCmdController(IRoverCommandService service)
    {
        _service = service;
    }

    [HttpPost("send")]
    [ProducesResponseType(typeof(SendResponse), StatusCodes.Status200OK)]
    public IActionResult Send([FromBody] SendRequest request)
    {
        var response = _service.ProcessRequest(request);
        return Ok(response);
    }
}
