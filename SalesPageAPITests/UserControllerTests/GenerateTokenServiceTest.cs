using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SalesPageAPI.Models;
using SalesPageAPI.Services.UserServices;

namespace SalesPageAPITests.UserControllerTests
{
    public class GenerateTokenServiceTest
    {

        private readonly IConfiguration _configuration;
        private readonly User _user;

        public GenerateTokenServiceTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                {"SymmetricSecurityKey", "this-is-a-secret-key-for-testing"}
                })
                .Build();

            _user = new User
            {
                Id = "1",
                UserName = "john",
                DayBirth = new DateTime(1980, 1, 1)
            };
        }

        [Fact]
        public void TokenServiceReturnTest()
        {
            // Arrange
            var tokenService = new TokenService(_configuration);

            // Act
            var token = tokenService.GenerateToken(_user);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            var claimsIdentity = principal.Identity as ClaimsIdentity;

            Assert.NotNull(validatedToken);
            Assert.NotNull(principal);
            Assert.NotNull(claimsIdentity);

            var usernameClaim = claimsIdentity.FindFirst("username");
            var idClaim = claimsIdentity.FindFirst("id");
            var dobClaim = claimsIdentity.FindFirst(ClaimTypes.DateOfBirth);

            Assert.NotNull(usernameClaim);
            Assert.NotNull(idClaim);
            Assert.NotNull(dobClaim);

            Assert.Equal(_user.UserName, usernameClaim.Value);
            Assert.Equal(_user.Id, idClaim.Value);
            Assert.Equal(_user.DayBirth.ToString(), dobClaim.Value);
        }
    }
}
