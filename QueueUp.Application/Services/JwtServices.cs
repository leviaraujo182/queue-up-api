using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class JwtServices(IConfiguration configuration) : IJwtServices
{
    public Task<string> GenerateToken(User user)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString())
        };
        
        var expires = DateTime.UtcNow.AddHours(2);

        
        var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return Task.FromResult(tokenString);
    }
}