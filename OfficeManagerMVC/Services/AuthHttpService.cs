using LanguageExt.Common;
using Microsoft.Extensions.Options;
using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Services
{
    public class AuthHttpService : IAuthHttpService
    {
        private readonly IBaseHttpService httpService;
        private readonly HttpData httpData;

        public AuthHttpService(
            IBaseHttpService httpService, 
            IOptions<HttpData> httpData)
        {
            this.httpService = httpService;
            this.httpData = httpData.Value;
        }

        public async ValueTask<ResponseDto> AssignRole(AssignRoleRequestDto assignRoleRequestDto)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.AuthAPI + "/api/v1/Auth/AssignRole",
                Data = assignRoleRequestDto
            });
        }

        public async ValueTask<ResponseDto> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.AuthAPI + "/api/v1/Auth/ChangePassword",
                Data = changePasswordRequest
            });
        }

        public async ValueTask<ResponseDto> Login(LoginRequestDto loginRequest)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.AuthAPI + "/api/v1/Auth/Login",
                Data = loginRequest
            });
        }

        public async ValueTask<ResponseDto> Register(RegistrationRequestDto registrationRequestDTO)
        {
            return await httpService.SendAsync(new RequestDto()
            {
                Method = ApiMethod.POST,
                Url = httpData.AuthAPI + "/api/v1/Auth/Register",
                Data = registrationRequestDTO
            });
        }
    }
}
