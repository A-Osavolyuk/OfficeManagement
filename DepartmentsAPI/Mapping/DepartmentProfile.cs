using AutoMapper;
using DepartmentsAPI.Models.DTOs;
using DepartmentsAPI.Models.Entities;

namespace DepartmentsAPI.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
