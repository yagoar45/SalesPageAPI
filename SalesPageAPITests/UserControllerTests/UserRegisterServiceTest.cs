using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Models;
using SalesPageAPI.Services.UserServices;

namespace SalesPageAPITests.UserControllerTests
{
    public class UserRegisterServiceTest
    {
        [Theory]
        [InlineData("johndoe", "1980-01-01", "P@ssword123", "P@ssword123")]
        [InlineData("yago-araujo", "1990-02-15", "password123", "password123")]
        [InlineData("bobsmith", "1975-06-30", "secret123", "secret123")]
        public async Task RegisterUserSuccessfully(string userName, DateTime birthDate,
            string password, string confirmPassword)
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                UserName = userName,
                DayBirth = birthDate,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var mockMapper = new Mock<IMapper>();
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var service = new UserRegisterService(mockMapper.Object, mockUserManager.Object);

            mockMapper.Setup(m => m.Map<User>(userDto)).Returns(new User());

            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), userDto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await service.Register(userDto);

            // Assert
            mockMapper.Verify(m => m.Map<User>(userDto), Times.Once);
            mockUserManager.Verify(m => m.CreateAsync(It.IsAny<User>(), userDto.Password), Times.Once);
        }


        [Theory]
        [InlineData("johndoe", "1980-01-01", "P@ssword123", "geqgqf")]
        [InlineData("janeDoe", "1990-02-15", "passWord123#", "eqgeqf")]
        [InlineData("bobsmith", "1975-06-30", "Secret123", "secret123")]
        public async Task RegisterUserFailure(string userName, DateTime birthDate,
            string password, string confirmPassword)
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                UserName = userName,
                DayBirth = birthDate,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var mockMapper = new Mock<IMapper>();
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var service = new UserRegisterService(mockMapper.Object, mockUserManager.Object);

            mockMapper.Setup(m => m.Map<User>(userDto)).Returns(new User());

            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), userDto.Password))
                .ReturnsAsync(IdentityResult.Failed());

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => service.Register(userDto));
        }
    }

}

