using AutoMapper;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Models.ViewModels;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService employeesService;
        private readonly IPositionsService positionsService;
        private readonly IMapper mapper;

        public EmployeesController(
            IEmployeesService employeesService,
            IPositionsService positionsService,
            IMapper mapper)
        {
            this.employeesService = employeesService;
            this.positionsService = positionsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesList()
        {
            var employeeResult = await employeesService.GetAll();

            if (employeeResult.IsSucceeded)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Employee>>(Convert.ToString(employeeResult.Result));
                return View(result);
            }
            else
            {
                ViewData["error"] = employeeResult.Message;
                return View(new List<Employee>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesListByPositionId(int id)
        {
            var employeeResult = await employeesService.GetAllByPositionId(id);

            if (employeeResult.IsSucceeded)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Employee>>(Convert.ToString(employeeResult.Result));
                return View(result);
            }
            else
            {
                ViewData["error"] = employeeResult.Message;
                return View(new List<Employee>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeResult = await employeesService.DeleteById(id);

            if (employeeResult.IsSucceeded)
                TempData["success"] = employeeResult.Message;
            else
                ViewData["error"] = employeeResult?.Message;

            return RedirectToAction("employeesList");
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var positionsResult = await positionsService.GetAll();

            if (positionsResult.IsSucceeded)
            {
                var positionsList = JsonConvert.DeserializeObject<IEnumerable<Position>>(positionsResult.Result.ToString());
                return View(new CreateEmployeeViewModel() { Positions = positionsList.ToList() });
            }
            else
            {
                TempData["error"] = positionsResult?.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = mapper.Map<EmployeeDto>(model);
                var employeeResult = await employeesService.Create(employee);

                if (employeeResult.IsSucceeded)
                {
                    TempData["success"] = $"Employee was successful created.";
                    return RedirectToAction("employeesList");
                }
                else
                {
                    TempData["error"] = employeeResult.Message;
                }

                ModelState.AddModelError("", employeeResult.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var employeeResult = await employeesService.GetById(id);
            var positionsResult = await positionsService.GetAll();

            if (employeeResult.IsSucceeded && positionsResult.IsSucceeded)
            {
                var positionsList = JsonConvert.DeserializeObject<IEnumerable<Position>>(positionsResult.Result.ToString());
                var employee = JsonConvert.DeserializeObject<Employee>(employeeResult.Result.ToString());

                var result = mapper.Map<UpdateEmployeeViewModel>(employee);
                result.Positions = positionsList.ToList();

                return View(result);
            }

            TempData["error"] = employeeResult.Message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var employeeDto = mapper.Map<EmployeeDto>(viewModel);
                var employeeResult = await employeesService.Update(viewModel.EmployeeId, employeeDto);

                if (employeeResult.IsSucceeded)
                {
                    ViewData["success"] = "Employee was successful updated.";
                    return RedirectToAction("employeesList");
                }

                ViewData["error"] = employeeResult.Message;
            }

            return View();
        }

    }
}
