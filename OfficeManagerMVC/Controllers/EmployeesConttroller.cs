using Microsoft.AspNetCore.Mvc;

namespace OfficeManagerMVC.Controllers
{
    public class EmployeesConttroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
