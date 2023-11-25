using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;

#nullable disable

namespace OfficeManagerMVC.Services
{
    public class DepartmentHttpService : IDepartmentHttpService
    {
        private readonly IBaseHttpService httpService;
        private readonly HttpData httpData;

        public DepartmentHttpService(IBaseHttpService httpService, HttpData httpData)
        {
            this.httpService = httpService;
            this.httpData = httpData;
        }

        public async ValueTask<ResponseDto> Create(DepartmentDto department)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.DepartmentsAPI + "/api/Departments",
                Data = department
            });
        }

        public async ValueTask<ResponseDto> DeleteById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.DELETE,
                Url = httpData.DepartmentsAPI + $"/api/Departments/{id}"
            });
        }

        public async ValueTask<ResponseDto> GetAll()
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.DepartmentsAPI + "/api/Departments",
            });
        }

        public async ValueTask<ResponseDto> GetById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.DepartmentsAPI + $"/api/Departments/{id}",
            });
        }

        public async ValueTask<ResponseDto> Update(int id, DepartmentDto department)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.PUT,
                Url = httpData.DepartmentsAPI + $"/api/Departments/{id}",
                Data = department
            });
        }
    }
}
