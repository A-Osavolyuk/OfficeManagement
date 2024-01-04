using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Models.ViewModels;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionsService positionsService;
        private readonly IMapper mapper;
        private readonly IDepartmentService departmentService;

        public PositionsController(
            IPositionsService positionsService,
            IMapper mapper,
            IDepartmentService departmentService)
        {
            this.positionsService = positionsService;
            this.mapper = mapper;
            this.departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> PositionsList()
        {
            var result = await positionsService.GetAll();

            if (result.IsSucceeded)
            {
                var positions = JsonConvert.DeserializeObject<IEnumerable<Position>>(result.Result.ToString());
                return View(positions);
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return View(new List<Position>());
        }

        [HttpGet]
        public async Task<IActionResult> PositionsListByDepartmentId(int id)
        {
            var result = await positionsService.GetAllByDepartmentId(id);

            if (result.IsSucceeded)
            {
                var positions = JsonConvert.DeserializeObject<IEnumerable<Position>>
                    (result.Result.ToString());

                return View(positions);
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return View(new List<Position>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await departmentService.GetAll();

            if (result.IsSucceeded)
            {
                var departments = JsonConvert.DeserializeObject<IEnumerable<Department>>(result.Result.ToString());

                return View(new CreatePositionViewModel() { Departments = departments.ToList() });
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var position = mapper.Map<PositionDto>(viewModel);
                var result = await positionsService.Create(position);

                if (result.IsSucceeded)
                {
                    TempData["success"] = $"Position was successful created.";
                    return RedirectToAction("positionsList", "positions");
                }
                else
                {
                    TempData["error"] = result.Message;
                }

                ModelState.AddModelError("", result.Message);
            }

            return View();
        }
    }
}
