using DepartmentsAPI.Models.DTOs;
using DepartmentsAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentsAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ILogger<DepartmentsController> logger;

        public DepartmentsController(IDepartmentsService departmentsService, ILogger<DepartmentsController> logger)
        {
            this.departmentsService = departmentsService;
            this.logger = logger;
        }

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetAllDepartment()
        {
            var result = await departmentsService.GetAll();

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation("Successful getting list of all departments.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning("Cannot get list of all departments. Status code 500.");
                    return StatusCode(500);
                });
        }

        [HttpGet("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> GetDepartmentById(int id)
        {
            var result = await departmentsService.GetById(id);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting department with id: {id}.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
                });
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateDepartment([FromBody] DepartmentDto department)
        {
            var result = await departmentsService.Create(department);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Department with name: {succ.DepartmentName} was successful create.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return BadRequest(fail.Message);
                });
        }

        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteDepartmentById(int id)
        {
            var result = await departmentsService.DeleteById(id);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Department with id: {id} was successful deleted.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Message = $"Department with id: {id} was deleted successful." });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
                });
        }

        [HttpPut("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateDepartment([FromBody] DepartmentDto department, int id)
        {
            var result = await departmentsService.Update(department, id);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Department with id: {succ.DepartmentId} was successful updated");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    if (fail is ValidationException exception)
                    {
                        logger.LogWarning(exception.Message);
                        return BadRequest(exception.Message);
                    }

                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
                });
        }
    }
}
