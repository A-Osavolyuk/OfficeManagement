using Microsoft.AspNetCore.Mvc;

namespace OfficeManagerMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
