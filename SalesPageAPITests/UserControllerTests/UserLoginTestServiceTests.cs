using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Models;
using SalesPageAPI.Services.UserServices;

namespace SalesPageAPITests.UserControllerTests
{
    public class UserLoginTestServiceTests
    {

        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<TokenService> _mockTokenService;
        private readonly UserLoginService _userLoginService;
        private readonly IConfiguration _configuration;
        public UserLoginTestServiceTests()
        {
            _mockSignInManager = new Mock<SignInManager<User>>(
                Mock.Of<UserManager<User>>(u => u.Users == new List<User>().AsQueryable()),
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null,
                null,
                null);

            _mockTokenService = new Mock<TokenService>();

            _userLoginService = new UserLoginService(_mockSignInManager.Object, _mockTokenService.Object);
        }

        [Theory]
        [InlineData("yago-araujo", "Senha123@")]
        [InlineData("lucas", "umaSenhaForte#1")]
        [InlineData("marcos", "OutraSenhaForte#2")]
        public async Task UserLoginSuccessfully(string userName, string password)
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object, null, null, null, null, null, null);
            var mockTokenService = new Mock<TokenService>();

            var loginUserDto = new LoginUserDto
            {
                UserName = userName,
                Password = password
            };

            var user = new User { UserName = loginUserDto.UserName, NormalizedUserName = loginUserDto.UserName.ToUpper() };
            mockSignInManager.Setup(x => x.UserManager.Users).Returns(new[] { user }.AsQueryable());

            var userLoginService = new UserLoginService(mockSignInManager.Object, mockTokenService.Object);

            // Act
            var result = await userLoginService.Login(loginUserDto);

            // Assert
            Assert.NotNull(result);
            mockTokenService.Verify(x => x.GenerateToken(user), Times.Once);
        }

        [Fact]
        public async Task UserLoginFailed()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            // Configurar o mock do UserStore para retornar um usuário
            mockUserStore.Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { UserName = "testuser" });

            // Criar o serviço com o UserManager mockado
            var service = new UserLoginService(new SignInManager<User>(userManager, null, null, null, null, null, null), new TokenService(_configuration));

            // Testar o caso de falha
            await Assert.ThrowsAsync<ApplicationException>(async () => await service.Login(new LoginUserDto { UserName = "testuser", Password = "wrongpassword" }));
        }
    }
}
