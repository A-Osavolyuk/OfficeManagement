using Microsoft.AspNetCore.Mvc;

namespace OfficeManagerMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        [HttpGet]
        public async ValueTask<IActionResult> DepartmentsList()
        {
            return View();
        }
    }
}
