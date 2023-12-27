using Microsoft.AspNetCore.Mvc;

namespace OfficeManagerMVC.Controllers
{
    public class PositionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
