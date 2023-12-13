using OfficeManagerBlazorServer.Services.Interfaces;

namespace OfficeManagerBlazorServer.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void CleanToken()
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Delete("JWT-Token");
        }

        public string? GetToken()
        {
            string? token;
            var isValid = httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("JWT-Token", out token);
            return isValid ? token : null;
        }

        public void SetToken(string token)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Append("JWT-Token", token);
        }
    }
}
