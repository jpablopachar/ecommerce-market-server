using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Logic.Service
{
    /// <summary>
    /// Implementación del servicio de generación de tokens JWT.
    /// </summary>
    /// <remarks>
    /// Este servicio se encarga de crear tokens JWT para la autenticación y autorización de usuarios en el sistema.
    /// </remarks>
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
        }

        public string CreateToken(User user, IList<string>? roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Name, user.Name!),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName!),
                new("username", user.UserName!),
            };

            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = credentials,
                Issuer = _configuration["Token:Issuer"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}