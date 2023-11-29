using AuthAPI.Models.DTOs;
using LanguageExt.Common;

namespace AuthAPI.Services.Interfaces
{
    public interface IAuthService
    {
        ValueTask<Result<UserDto>> Register(RegistrationRequestDto registrationRequestDTO);
        ValueTask<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDTO);
        ValueTask<Result<string>> AssignRole(string email, string roleName);
        ValueTask<Result<bool>> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest);

    }
}
