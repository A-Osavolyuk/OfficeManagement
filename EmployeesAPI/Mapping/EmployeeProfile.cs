using AutoMapper;
using EmployeesAPI.Models.DTOs;
using EmployeesAPI.Models.Entities;

namespace EmployeesAPI.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
