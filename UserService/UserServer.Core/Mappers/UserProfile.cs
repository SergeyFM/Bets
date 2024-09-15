using AutoMapper;
using UserServer.Core.DTO;
using UserServer.Core.Entities;

namespace UserServer.Core.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap(); // Маппинг между User и UserDto
            CreateMap<User, ResponceUserDto>().ReverseMap();
        }
    }
}
