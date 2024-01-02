using Microsoft.Extensions.Options;
using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;

#nullable disable

namespace OfficeManagerMVC.Services
{
    public class PositionsService : IPositionsService
    {
        private readonly IBaseService httpService;
        private readonly HttpData httpData;

        public PositionsService(IBaseService httpService, IOptions<HttpData> httpData)
        {
            this.httpService = httpService;
            this.httpData = httpData.Value;
        }

        public async ValueTask<ResponseDto> Create(PositionDto positionDto)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.PositionsAPI + "/api/v1/Positions",
                Data = positionDto,
            });
        }

        public async ValueTask<ResponseDto> DeleteById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.DELETE,
                Url = httpData.PositionsAPI + $"/api/v1/Positions/{id}",
            });
        }

        public async ValueTask<ResponseDto> GetAll()
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + "/api/v1/Positions",
            });
        }

        public async ValueTask<ResponseDto> GetAllByDepartmentId(int departmentId)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Positions/GetByDepartmentId/{departmentId}",
            });
        }

        public async ValueTask<ResponseDto> GetById(int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Positions/{id}",
            });
        }

        public async ValueTask<ResponseDto> GetByName(string name)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.GET,
                Url = httpData.PositionsAPI + $"/api/v1/Positions/{name}",
            });
        }

        public async ValueTask<ResponseDto> Update(PositionDto positionDto, int id)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.PUT,
                Url = httpData.PositionsAPI + $"/api/v1/Positions/{id}",
                Data = positionDto,
            });
        }
    }
}
