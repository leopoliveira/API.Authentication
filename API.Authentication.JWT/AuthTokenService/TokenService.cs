using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using API.Authentication.Core.Entities;

using Microsoft.IdentityModel.Tokens;

namespace API.Authentication.JWT.AuthTokenService
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly string _jwtKey;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _jwtKey = _config["JWT-Secret"] ?? throw new ArgumentNullException("JWT Secret is missing.");
        }

        public string GenerateToken()
        {
            var encodedKey = Encoding.UTF8.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
