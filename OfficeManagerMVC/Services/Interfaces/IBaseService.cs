using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
