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
    

    
}