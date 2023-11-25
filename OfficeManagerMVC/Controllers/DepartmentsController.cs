using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentHttpService departmentHttpService;

        public DepartmentsController(IDepartmentHttpService departmentHttpService)
        {
            this.departmentHttpService = departmentHttpService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> DepartmentsList()
        {
            var result = await departmentHttpService.GetAll();

            if (result.IsSucceeded)
            {
                var departments = JsonConvert.DeserializeObject<IEnumerable<Department>>(result.Result.ToString());
                return View(departments);
            }

            return View();
        }
    }
}
