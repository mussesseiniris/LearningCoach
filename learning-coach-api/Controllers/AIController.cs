using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
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
    /// Sends user's message and learning sessions as system prompt to Claude and returns Claude's response
    /// </summary>
    /// <param name="userMessage"></param>
    [HttpPost("ask")]
    public async Task<ActionResult<string>> Ask([FromBody] string userMessage)
    {
        var learningsessions = await _context.LearningSessions.Include(s=>s.Subject).ToListAsync();
        var sessionText = string.Join("\n",learningsessions.Select(s => $"Subject:{s.Subject.Name},Duration:{s.Duration},Note:{s.Note}"));
        var subjects = await _context.Subjects.ToListAsync();
        var subjectText = string.Join("\n",subjects.Select(s=>$"Subject:{s.Name},Duration:{s.Duration},Goal:{s.Goal}"));
        var systemPrompt = $"You are a strict and supportive learning coach. Based on the user's learning progress, create concise study plans and quiz the user on what they have learned.\n\nSubjects:\n{subjectText}\n\nLearning Sessions:\n{sessionText}";
        var chatMessages = await _context.ChatMessages
            .OrderByDescending(m=>m.Time).Take(6).OrderBy(m=>m.Time).ToListAsync();
        var claudeResponse = await _claudeService.AskClaudeAsync(chatMessages,userMessage,systemPrompt);
        _context.ChatMessages.Add(new ChatMessage
        {
           MessageRole = "user",
           MessageContent =userMessage,
           Time = DateTime.UtcNow,
           UserID = 1,
        });
        
        _context.ChatMessages.Add(new ChatMessage
        {
            MessageRole = "assistant",
            MessageContent =claudeResponse,
            Time = DateTime.UtcNow,
            UserID = 1,
        });
        await _context.SaveChangesAsync();
        return Ok(claudeResponse);
    }
}