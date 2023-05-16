using AutoMapper;
using SalesPageAPI.Data.DTOS.UserDtos;
using SalesPageAPI.Models;

namespace SalesPageAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
