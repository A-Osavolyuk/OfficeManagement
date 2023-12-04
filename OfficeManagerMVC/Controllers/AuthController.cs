using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OfficeManagerMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthHttpService authHttpService;
        private readonly ITokenProvider tokenProvider;

        public AuthController(
            IAuthHttpService authHttpService,
            ITokenProvider tokenProvider)
        {
            this.authHttpService = authHttpService;
            this.tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await authHttpService.Register(registrationRequest);

                if (result.IsSucceeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var result = await authHttpService.Login(loginRequestDto);

                if (result.IsSucceeded && result.Result != null)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(
                        Convert.ToString(result.Result));

                    await SignInUser(loginResponse);
                    tokenProvider.SetToken(loginResponse.Token);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.CleanToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(loginResponseDto.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                token.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
