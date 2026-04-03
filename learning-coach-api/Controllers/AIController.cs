using LearningCoachAPI.Data;
using LearningCoachAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningCoachAPI.Controllers;

/// <summary>
/// Handles AI requests, using ClaudeService to integrate with Claude API
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly ClaudeService _claudeService;
    private readonly AppDbContext _context;

  
    public AIController(ClaudeService claudeService,AppDbContext context)
    {
        _claudeService = claudeService;
        _context = context;
    }
    /// <summary>
    /// Sends user's message to Claude and returns Claude's response
    /// </summary>
    /// <param name="claudeService"></param>
    [HttpPost("ask")]
    public async Task<ActionResult<string>> Ask([FromBody] string userMessage)
    {
        var learningsessions = await _context.LearningSessions.Include(s=>s.Subject).ToListAsync();
        var sessionText = String.Join("\n", learningsessions.Select(s => $"Subject:{s.Subject.Name},Duration:{s.Duration},Note:{s.Note}"));
        var claudeResponse = await _claudeService.AskClaudeAsync(userMessage,sessionText);
        return Ok(claudeResponse);
    }
    
    
}