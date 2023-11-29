using AuthAPI.Models.DTOs;
using AuthAPI.Models.Entities;
using AutoMapper;

namespace AuthAPI.Mapping
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(m => m.UserId, o => o.MapFrom(src => src.Id))
                .ForMember(m => m.UserName, o => o.MapFrom(src => src.UserName))
                .ForMember(m => m.Email, o => o.MapFrom(src => src.Email))
                .ForMember(m => m.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber));
        }
    }
}
