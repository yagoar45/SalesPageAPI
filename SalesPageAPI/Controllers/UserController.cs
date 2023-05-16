using Microsoft.AspNetCore.Mvc;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Services.UserServices;

namespace SalesPageAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {

        private readonly UserRegisterService _registerUser;
        private readonly UserLoginService _loginUser;

        public UserController(UserRegisterService registerService, UserLoginService loginUser)
        {
            _registerUser = registerService;
            _loginUser = loginUser;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
        {
            await _registerUser.Register(userDto);
            return Ok("Usuário cadastrado com sucesso");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(LoginUserDto userLoginDto)
        {
            var token = await _loginUser.Login(userLoginDto);
            return Ok(token);
        }

    }
}
