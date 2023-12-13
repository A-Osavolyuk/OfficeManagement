using OfficeManagerBlazorServer.Models.DTOs;

namespace OfficeManagerBlazorServer.Services.Interfaces
{
    public interface IBaseHttpService
    {
        Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
