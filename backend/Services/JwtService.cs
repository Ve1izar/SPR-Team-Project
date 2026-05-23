using LocalDriveApi.Models;
using LocalDriveApi.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocalDriveApi.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GetAccessToken(User user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
            {
                throw new ArgumentNullException("JWT secret key is null");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim("userName", user.UserName ?? string.Empty),
                new Claim("email", user.Email ?? string.Empty),
                new Claim("firstName", user.FirstName ?? string.Empty),
                new Claim("lastName", user.LastName ?? string.Empty),
                new Claim("phoneNumber", user.PhoneNumber ?? string.Empty)
            };

            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var signingKey = new SymmetricSecurityKey(secretKeyBytes);

            var credentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}