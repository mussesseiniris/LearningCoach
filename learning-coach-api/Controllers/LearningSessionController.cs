using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LearningCoachAPI.Controllers;
[ApiController]
[Route("api/[controller]")]

public class LearningSessionController : ControllerBase{

private readonly AppDbContext _context ;

public LearningSessionController(AppDbContext context){

_context = context;
    
}

[HttpGet]
public async Task<ActionResult<List<LearningSession>>> GetLearningSession(){
var LearningSessions = await _context.LearningSessions.ToListAsync();
return LearningSessions;
}

[HttpPost]
public async Task<ActionResult<LearningSession>> AddLearningSession(LearningSession learningSession){
    
 _context.LearningSessions.Add(learningSession);
await _context.SaveChangesAsync();
return Ok(learningSession); 

}

[HttpDelete]
public async Task<ActionResult<LearningSession>> DeleteLearningSession(int id){
    
  var learningSession = await _context.LearningSessions.FindAsync(id);
if (learningSession == null){
    return NotFound();
}
_context.LearningSessions.Remove(learningSession);
await _context.SaveChangesAsync();
return NoContent();  
}

[HttpPut("{id}")]
public async Task<ActionResult<LearningSession>> PutLearningSession(int id, LearningSession learningSession){

var existingLearningSession = await _context.LearningSessions.FindAsync(id);
if(existingLearningSession == null){

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
