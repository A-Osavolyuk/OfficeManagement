using AutoMapper;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;

namespace OfficeManagerMVC.Mapping
{
    public class DepartmentsProfile : Profile
    {
        public DepartmentsProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
