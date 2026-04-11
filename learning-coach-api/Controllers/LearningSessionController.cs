using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LearningCoachAPI.Controllers;

/// <summary>
/// Handles learning sessions CRUD-Create, Read, Update, Delete.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LearningSessionController : ControllerBase
{
    private readonly AppDbContext _context;

    public LearningSessionController(AppDbContext context)
    {
        _context = context;
    }
/// <summary>
/// Gets a list of learning sessions from the database based on the subjectId
/// </summary>
/// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<LearningSession>>> GetLearningSession(int? subjectId)
    {
        // var LearningSessions = await _context.LearningSessions.ToListAsync();
        var LearningSessions = await _context.LearningSessions.Where(ls => ls.SubjectId == subjectId).ToListAsync();
        return LearningSessions;
    }

/// <summary>
/// Adds a new learning session into the database.
/// </summary>
/// <param name="learningSession"></param>
/// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<LearningSession>> AddLearningSession(LearningSession learningSession)
    {
        _context.LearningSessions.Add(learningSession);
        await _context.SaveChangesAsync();
        return Ok(learningSession);
    }

/// <summary>
/// Deletes a learning session from the database.
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LearningSession>> DeleteLearningSession(int id)
    {
        var learningSession = await _context.LearningSessions.FindAsync(id);
        if (learningSession == null)
        {
            return NotFound();
        }

        _context.LearningSessions.Remove(learningSession);
        await _context.SaveChangesAsync();
        return NoContent();
    }

/// <summary>
/// Updates an existing learning session in the database.
/// </summary>
/// <param name="id"></param>
/// <param name="learningSession"></param>
/// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<LearningSession>> PutLearningSession(int id, LearningSession learningSession)
    {
        var existingLearningSession = await _context.LearningSessions.FindAsync(id);
        if (existingLearningSession == null)
        {
            return NotFound();
        }

        existingLearningSession.Duration = learningSession.Duration;
        existingLearningSession.StartTime = learningSession.StartTime;
        existingLearningSession.EndTime = learningSession.EndTime;
        existingLearningSession.Note = learningSession.Note;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}