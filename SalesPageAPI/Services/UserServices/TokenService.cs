using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SalesPageAPI.Models;

namespace SalesPageAPI.Services.UserServices
{
    public class TokenService
    {

        // inicialize the configuration for access to secrets
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Use a token for acess to the content after autentication 
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.DayBirth.ToString()),
            };

            SymmetricSecurityKey key = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes("GEOQGNQOGINEQOUGFQWEFHF"));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(24),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
