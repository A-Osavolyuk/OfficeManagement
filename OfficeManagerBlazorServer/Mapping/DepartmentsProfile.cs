using AutoMapper;
using OfficeManagerBlazorServer.Models.DTOs;
using OfficeManagerBlazorServer.Models.Entities;

namespace OfficeManagerBlazorServer.Mapping
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
