using Blazored.LocalStorage;
using Blazored.SessionStorage;
using OfficeManagerBlazorServer.Services.Interfaces;

namespace OfficeManagerBlazorServer.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ISessionStorageService sessionStorageService;
        private readonly ILocalStorageService localStorageService;

        public TokenProvider(ISessionStorageService sessionStorageService, ILocalStorageService localStorageService)
        {
            this.sessionStorageService = sessionStorageService;
            this.localStorageService = localStorageService;
        }

        public async ValueTask CleanTokenAsync()
        {
            await sessionStorageService.ClearAsync();
        }

        public async ValueTask<string> GetToken()
        {
            //string? token = await sessionStorageService.GetItemAsStringAsync("JWT-Token");
            //return string.IsNullOrEmpty(token)? null : token;

            string? token = await localStorageService.GetItemAsStringAsync("JWT-Token");
            return string.IsNullOrEmpty(token) ? null : token;
        }

        public async ValueTask SetToken(string token)
        {
            //await sessionStorageService.SetItemAsStringAsync("JWT-Token", token);
            await localStorageService.SetItemAsStringAsync("JWT-Token", token);
        }
    }
}
