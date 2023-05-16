using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Models;

namespace SalesPageAPI.Services.UserServices
{

        public class UserRegisterService
        {
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;

            public UserRegisterService(IMapper mapper, UserManager<User> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }


            public async Task Register(CreateUserDto userDto)
            {
                User user = _mapper.Map<User>(userDto);

                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    throw new ApplicationException("Falha ao cadastrar o usuário");
                }
            }
        }
    }
