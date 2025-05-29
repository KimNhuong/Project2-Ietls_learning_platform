using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeepSeekRequestsController : ControllerBase
{
    private readonly IDeepSeekRequestService _deepSeekRequestService;

    public DeepSeekRequestsController(IDeepSeekRequestService deepSeekRequestService)
    {
        _deepSeekRequestService = deepSeekRequestService;
    }

    [HttpPost("prompt")]
    public async Task<ActionResult<string>> SendPrompt([FromBody] string prompt)
    {
        var result = await _deepSeekRequestService.SendPromptAsync(prompt);
        return Ok(result);
    }
}