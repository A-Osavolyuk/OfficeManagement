using AuthAPI.Models.DTOs;
using AuthAPI.Models.Entities;
using AutoMapper;

namespace AuthAPI.Mapping
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationRequestDto, AppUser>()
                .ForMember(m => m.UserName, o => o.MapFrom(src => src.UserName))
                .ForMember(m => m.NormalizedUserName, o => o.MapFrom(src => src.UserName.ToUpper()))
                .ForMember(m => m.Email, o => o.MapFrom(src => src.Email))
                .ForMember(m => m.NormalizedEmail, o => o.MapFrom(src => src.Email.ToUpper()))
                .ForMember(m => m.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber));
        }
    }
}
