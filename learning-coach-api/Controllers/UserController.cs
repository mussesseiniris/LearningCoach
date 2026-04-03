using LearningCoachAPI.Data;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningCoachAPI.Controllers;
/// <summary>
/// Handles user data CRUD operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }
/// <summary>
/// Gets a list of users.
/// </summary>
/// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUser()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

/// <summary>
/// Adds a new user to the database.
/// </summary>
/// <param name="user"></param>
/// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

}