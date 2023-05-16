using Microsoft.AspNetCore.Identity;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Models;

namespace SalesPageAPI.Services.UserServices
{
    public class UserLoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public UserLoginService(SignInManager<User> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<string> Login(LoginUserDto userLoginDto)
        {
            SignInResult result = await _signInManager
                .PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("O usuário não foi autenticado");
            }

            var user = _signInManager
                       .UserManager
                       .Users
                       .FirstOrDefault(user => user.NormalizedUserName == userLoginDto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
