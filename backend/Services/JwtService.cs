using LocalDriveApi.Models;
using LocalDriveApi.Settings;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;



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
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey) ||
                string.IsNullOrEmpty(_jwtSettings.Issuer) ||
                string.IsNullOrEmpty(_jwtSettings.Audience))
            {
                throw new ArgumentNullException("Jwt settings are not configured");
            }

            var claims = new List<Claim>
    {
        new Claim("userId", user.Id.ToString())
    };

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim("userName", user.UserName));

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim("email", user.Email));

            if (!string.IsNullOrEmpty(user.FirstName))
                claims.Add(new Claim("firstName", user.FirstName));

            if (!string.IsNullOrEmpty(user.LastName))
                claims.Add(new Claim("lastName", user.LastName));

            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim("phoneNumber", user.PhoneNumber));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
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
