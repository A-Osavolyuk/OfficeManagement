using Microsoft.Extensions.Options;
using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;

#nullable disable

namespace OfficeManagerMVC.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseService httpService;
        private readonly HttpData httpData;

        public DepartmentService(IBaseService httpService, IOptions<HttpData> httpData)
        {
            this.httpService = httpService;
            this.httpData = httpData.Value;
        }

        public async ValueTask<ResponseDto> Create(DepartmentDto department)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.DepartmentsAPI + "/api/v1/Departments",
                Data = department,
            });
        }

        public async ValueTask<ResponseDto> DeleteById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.DELETE,
                Url = httpData.DepartmentsAPI + $"/api/v1/Departments/{id}"
            });
        }

        public async ValueTask<ResponseDto> GetAll()
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.DepartmentsAPI + "/api/v1/Departments",
            });
        }

        public async ValueTask<ResponseDto> GetById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.DepartmentsAPI + $"/api/v1/Departments/{id}",
            });
        }

        public async ValueTask<ResponseDto> Update(int id, DepartmentDto department)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.PUT,
                Url = httpData.DepartmentsAPI + $"/api/v1/Departments/{id}",
                Data = department
            });
        }
    }
}
