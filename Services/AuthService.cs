using System.Security.Claims;
using System.Text;
using System;
using TaskManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TaskManagement.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;

        public AuthService(IConfiguration config, AppDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>();

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User ValidateUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }

}
