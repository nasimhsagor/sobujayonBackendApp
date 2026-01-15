using sobujayonApp.Core.ServiceContracts;
using sobujayonApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Guid userId, string email, string personName)
        {
            try
            {
                // Get JWT settings from appsettings.json
                var key = _configuration["JWT:Key"];
                var issuer = _configuration["JWT:Issuer"];
                var audience = _configuration["JWT:Audience"];

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    throw new InvalidOperationException("JWT configuration is missing or invalid");
                }

                // Create security key from the secret key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                // Create signing credentials
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create claims (payload) for the token
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Subject (User ID)
                    new Claim(JwtRegisteredClaimNames.Email, email),           // Email
                    new Claim(JwtRegisteredClaimNames.Name, personName),       // Name
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID (unique identifier)
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64) // Issued at
                };

                // Create the JWT token
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(120), // Token valid for 2 hours
                    signingCredentials: credentials
                );

                // Serialize the token to string
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating JWT token: {ex.Message}", ex);
            }
        }
    }
}