using Blazored.SessionStorage;
using OfficeManagerBlazorServer.Services.Interfaces;

namespace OfficeManagerBlazorServer.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ISessionStorageService sessionStorageService;

        public TokenProvider(ISessionStorageService sessionStorageService)
        {
            this.sessionStorageService = sessionStorageService;
        }

        public async ValueTask CleanTokenAsync()
        {
            await sessionStorageService.ClearAsync();
        }

        public async ValueTask<string> GetToken()
        {
            string? token = await sessionStorageService.GetItemAsStringAsync("JWT-Token");
            return string.IsNullOrEmpty(token)? null : token;
        }

        public async ValueTask SetToken(string token)
        {
            await sessionStorageService.SetItemAsStringAsync("JWT-Token", token);
        }
    }
}
