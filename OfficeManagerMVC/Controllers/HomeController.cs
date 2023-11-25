using Microsoft.AspNetCore.Mvc;

namespace OfficeManagerMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
