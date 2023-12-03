using LanguageExt.Common;
using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IAuthHttpService
    {
        ValueTask<ResponseDto> Register(RegistrationRequestDto registrationRequestDTO);
        ValueTask<ResponseDto> Login(LoginRequestDto loginRequestDTO);
        ValueTask<ResponseDto> AssignRole(string email, string roleName);
        ValueTask<ResponseDto> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest);
    }
}
