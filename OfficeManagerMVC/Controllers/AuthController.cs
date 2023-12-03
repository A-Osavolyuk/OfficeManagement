using Microsoft.AspNetCore.Mvc;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthHttpService authHttpService;

        public AuthController(IAuthHttpService authHttpService)
        {
            this.authHttpService = authHttpService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationRequestDto());
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
    }
}
