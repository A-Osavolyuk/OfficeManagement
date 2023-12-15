using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using OfficeManagerBlazorServer.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OfficeManagerBlazorServer
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthHttpService authService;
        private readonly IHttpClientFactory clientFactory;

        public JwtAuthStateProvider(IHttpClientFactory clientFactory, IAuthHttpService authService)
        {
            this.authService = authService;
            this.clientFactory = clientFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var principal = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(principal);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public async void MarkUserAsAssigned(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                                        jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                                        jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                                        jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                                        jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            var principal = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(principal);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
    }
}
