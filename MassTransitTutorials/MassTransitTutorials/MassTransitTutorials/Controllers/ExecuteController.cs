using CommandExecutor;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitTutorials.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController : ControllerBase
{
    private readonly ILogger<ExecuteController> _logger;
    private readonly IRequestClient<ExecuteCommand> _client;

    public ExecuteController(ILogger<ExecuteController> logger, 
        IRequestClient<ExecuteCommand> client)
    {
        _logger = logger;
        _client = client;
    }

    [HttpGet(Name = "Start")]
    public async Task<IActionResult> Get()
    {
        var result = await _client.GetResponse<ExecuteResponse>(new
        {
            Name = "Are you here?"
        });
        return Ok();
    }
}