using OfficeManagerBlazorServer.Models.Enums;

namespace OfficeManagerBlazorServer.Models.DTOs
{
    public class RequestDto
    {
        public ApiMethod Method { get; set; } = ApiMethod.GET;
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
