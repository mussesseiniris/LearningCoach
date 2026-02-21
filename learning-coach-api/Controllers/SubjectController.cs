using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningCoachAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController: ControllerBase
{
private readonly AppDbContext _context;

    public SubjectController(AppDbContext context)
    {
        _context=context;
        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Subject>>> GetSubjects()
    {
        var subjects = await _context.Subjects.ToListAsync();
        return subjects;
    }
    

[HttpPost]
public async Task<ActionResult<Subject>> CreateSubject (Subject subject)
{
    _context.Subjects.Add(subject);
    
    await _context.SaveChangesAsync();
    return Ok(subject);
    
}


[HttpDelete]
public async Task<ActionResult<Subject>> DeleteSubject (int id)
{

    var subject = await _context.Subjects.FindAsync(id);
    if(subject == null)return NotFound();
    
    _context.Subjects.Remove(subject);
    await _context.SaveChangesAsync();
    return NoContent();
}

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

