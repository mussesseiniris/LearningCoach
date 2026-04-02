using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LearningCoachAPI.Controllers;
/// <summary>
/// Handles Subjects CRUD.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubjectController: ControllerBase
{
private readonly AppDbContext _context;

    public SubjectController(AppDbContext context)
    {
        _context=context;
        
    }
    /// <summary>
    /// Gets a list of subjects from the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Subject>>> GetSubjects()
    {
        var subjects = await _context.Subjects.ToListAsync();
        return subjects;
    }
    
/// <summary>
///Add a new subject to the database.
/// </summary>
/// <param name="subject"></param>
/// <returns></returns>
[HttpPost]
public async Task<ActionResult<Subject>> CreateSubject (Subject subject)
{
    _context.Subjects.Add(subject);
    
    await _context.SaveChangesAsync();
    return Ok(subject);
    
}

/// <summary>
/// Delete a subject from the database.
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
[HttpDelete("{id}")]
public async Task<ActionResult<Subject>> DeleteSubject (int id)
{

    var subject = await _context.Subjects.FindAsync(id);
    if(subject == null)return NotFound();
    
    _context.Subjects.Remove(subject);
    await _context.SaveChangesAsync();
    return NoContent();
}
/// <summary>
/// Update the subject existing in the database by ID.
/// </summary>
/// <param name="id"></param>
/// <param name="subject"></param>
/// <returns></returns>
[HttpPut("{id}")]
public async Task<ActionResult<Subject>> PutSubject(int id, Subject subject){

var existingSubject = await _context.Subjects.FindAsync(id);
if (existingSubject == null) return NotFound();

existingSubject.Name = subject.Name;
existingSubject.Duration = subject.Duration;
existingSubject.Deadline = subject.Deadline;
existingSubject.Priority = subject.Priority;
    
await _context.SaveChangesAsync();
return Ok(existingSubject);
}
    
}

