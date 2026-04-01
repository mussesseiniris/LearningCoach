using LearningCoachAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningCoachAPI.Controllers;

/// <summary>
/// Handles AI requests, using ClaudeService to integrate with Claude API
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly ClaudeService _claudeService;

  
    public AIController(ClaudeService claudeService)
    {
        _claudeService = claudeService;
    }
    /// <summary>
    /// Sends user's message to Claude and returns Claude's response
    /// </summary>
    /// <param name="claudeService"></param>
    [HttpPost("ask")]
    public async Task<ActionResult<string>> Ask([FromBody] string userMessage)
    {
        var claudeResponse = await _claudeService.AskClaudeAsync(userMessage);
        return Ok(claudeResponse);
    }
}