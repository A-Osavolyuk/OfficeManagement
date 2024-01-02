using Microsoft.Extensions.Options;
using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;

#nullable disable

namespace OfficeManagerMVC.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IBaseService httpService;
        private readonly HttpData httpData;

        public EmployeesService(IBaseService httpService, IOptions<HttpData> httpData)
        {
            this.httpService = httpService;
            this.httpData = httpData.Value;
        }

        public async ValueTask<ResponseDto> Create(EmployeeDto employeeDto)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.PositionsAPI + "/api/v1/Employees",
                Data = employeeDto,
            });
        }

        public async ValueTask<ResponseDto> DeleteById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.DELETE,
                Url = httpData.PositionsAPI + $"/api/v1/Employees/{id}",
            });
        }

        public async ValueTask<ResponseDto> GetAll()
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + "/api/v1/Employees",
            });
        }

        public async ValueTask<ResponseDto> GetAllByPositionId(int positionId)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Employees/GetEmployeesByPositionId/{positionId}",
            });
        }

        public async ValueTask<ResponseDto> GetByEmail(string email)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Employees/{email}",
            });
        }

        public async ValueTask<ResponseDto> GetById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Employees/{id}",
            });
        }

        public async ValueTask<ResponseDto> Update(int id, EmployeeDto employeeDto)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.PUT,
                Url = httpData.PositionsAPI + $"/api/v1/Employees/{id}",
                Data = employeeDto,
            });
        }
    }
}
