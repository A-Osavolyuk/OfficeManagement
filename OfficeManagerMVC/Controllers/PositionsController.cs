﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeManagerMVC.Models.Entities;
using OfficeManagerMVC.Services.Interfaces;

namespace OfficeManagerMVC.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionsService positionsService;
        private readonly IMapper mapper;

        public PositionsController(IPositionsService positionsService, IMapper mapper)
        {
            this.positionsService = positionsService;
            this.mapper = mapper;
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
    }
}
