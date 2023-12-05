using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentHttpService departmentHttpService;
        private readonly IMapper mapper;

        public DepartmentsController(IDepartmentHttpService departmentHttpService, IMapper mapper)
        {
            this.departmentHttpService = departmentHttpService;
            this.mapper = mapper;
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
            else
            {
                TempData["error"] = result.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Create(DepartmentDto department)
        {
            if (ModelState.IsValid)
            {
                var result = await departmentHttpService.Create(department);

                if (result.IsSucceeded)
                {
                    return RedirectToAction("departmentsList", "departments");
                }
                else
                {
                    TempData["error"] = result.Message;
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        [HttpGet]
        public async ValueTask<IActionResult> Update(int id)
        {
            var result = await departmentHttpService.GetById(id);

            if (result.IsSucceeded)
            {
                var department = JsonConvert.DeserializeObject<Department>(result.Result.ToString());
                return View(department);
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Update(Department department)
        {
            if (ModelState.IsValid)
            {
                var result = await departmentHttpService.Update(department.DepartmentId, mapper.Map<DepartmentDto>(department));

                if (result.IsSucceeded)
                {
                    return RedirectToAction("departmentsList", "departments");
                }
                else
                {
                    TempData["error"] = result.Message;
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        [HttpGet]
        public async ValueTask<IActionResult> Delete(int id)
        {
            var result = await departmentHttpService.DeleteById(id);

            if (result.IsSucceeded)
            {
                return RedirectToAction("departmentsList", "departments");
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return NotFound();
        }
    }
}
