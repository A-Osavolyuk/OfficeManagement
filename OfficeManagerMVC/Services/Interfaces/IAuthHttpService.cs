using LanguageExt.Common;
using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IAuthHttpService
    {
        ValueTask<Result<UserDto>> Register(RegistrationRequestDto registrationRequestDTO);
        ValueTask<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDTO);
        ValueTask<Result<string>> AssignRole(string email, string roleName);
        ValueTask<Result<bool>> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest);
    }
}
