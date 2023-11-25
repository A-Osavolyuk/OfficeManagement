using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IBaseHttpService
    {
        Task<ResponseDto?> SendAsync(RequestDto request);
    }
}
