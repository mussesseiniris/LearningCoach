using Anthropic.Models.Messages;
using LearningCoachAPI.Data;
using LearningCoachAPI.DTO;
using LearningCoachAPI.Models;
using LearningCoachAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LearningCoachAPI.Controllers;

/// <summary>
/// Handles user authentication including registration and login.
/// Returns a JWT token on successful login.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private AppDbContext _dbContext;
    private TokenService _tokenService;

    public AuthController(AppDbContext dbContext,TokenService tokenService)
    
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }
    
    // Register a new user
    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var user = new User { Email = registerDto.Email, Name = registerDto.Name, PasswordHash = passwordHash };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    // Login and return a JWT token
    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginDto loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new { Message = token });
    }
}