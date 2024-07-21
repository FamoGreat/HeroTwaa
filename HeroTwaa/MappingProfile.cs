using AutoMapper;
using HeroTwaa.Models.DTOs;
using HeroTwaa.Models;

namespace HeroTwaa
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap();
            CreateMap<RegisterDTO, ApplicationUser>()
                .ReverseMap();
            CreateMap<UpdateUserDTO, ApplicationUser>()
                .ReverseMap();
        }
    }
}
