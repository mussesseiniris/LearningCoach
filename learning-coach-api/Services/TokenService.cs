using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearningCoachAPI.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace LearningCoachAPI.Services;
/// <summary>
/// Handles JWT token generation for authenticated users.
/// </summary>
public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Generates a JWT token containing the user's ID and email.
    /// Token expires after 7 days.
    /// </summary>
    public string GenerateToken(User user)
    {
        // Pack user info into token claims
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        // Build signing key from config secret
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // Create and return the signed token
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}