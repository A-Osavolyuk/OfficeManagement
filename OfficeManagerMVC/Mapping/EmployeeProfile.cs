using AutoMapper;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Models.ViewModels;

namespace OfficeManagerMVC.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, CreateEmployeeViewModel>()
                .ForMember(x => x.FirstName, o => o.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(src => src.LastName))
                .ForMember(x => x.Gender, o => o.MapFrom(src => src.Gender))
                .ForMember(x => x.Email, o => o.MapFrom(src => src.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(src => src.DateOfBirth))
                .ForMember(x => x.DateOfHire, o => o.MapFrom(src => src.DateOfHire))
                .ForMember(x => x.PositionId, o => o.MapFrom(src => src.PositionId));

            CreateMap<CreateEmployeeViewModel, EmployeeDto>()
                .ForMember(x => x.FirstName, o => o.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(src => src.LastName))
                .ForMember(x => x.Gender, o => o.MapFrom(src => src.Gender))
                .ForMember(x => x.Email, o => o.MapFrom(src => src.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(src => src.DateOfBirth))
                .ForMember(x => x.DateOfHire, o => o.MapFrom(src => src.DateOfHire))
                .ForMember(x => x.PositionId, o => o.MapFrom(src => src.PositionId));

            CreateMap<Employee, UpdateEmployeeViewModel>()
                .ForMember(x => x.EmployeeId, o => o.MapFrom(src => src.EmployeeId))
                .ForMember(x => x.FirstName, o => o.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(src => src.LastName))
                .ForMember(x => x.Gender, o => o.MapFrom(src => src.Gender))
                .ForMember(x => x.Email, o => o.MapFrom(src => src.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(src => src.DateOfBirth))
                .ForMember(x => x.DateOfHire, o => o.MapFrom(src => src.DateOfHire))
                .ForMember(x => x.PositionId, o => o.MapFrom(src => src.PositionId));

            CreateMap<UpdateEmployeeViewModel, EmployeeDto>()
                .ForMember(x => x.FirstName, o => o.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(src => src.LastName))
                .ForMember(x => x.Gender, o => o.MapFrom(src => src.Gender))
                .ForMember(x => x.Email, o => o.MapFrom(src => src.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(src => src.DateOfBirth))
                .ForMember(x => x.DateOfHire, o => o.MapFrom(src => src.DateOfHire))
                .ForMember(x => x.PositionId, o => o.MapFrom(src => src.PositionId));
        }
    }
}
