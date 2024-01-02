using EmployeesAPI.Models.DTOs;
using EmployeesAPI.Models.Entities;
using EmployeesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService employeesService;
        private readonly ILogger<EmployeesController> logger;

        public EmployeesController(
            IEmployeesService employeesService, 
            ILogger<EmployeesController> logger)
        {
            this.employeesService = employeesService;
            this.logger = logger;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var result = await employeesService.GetAll();

            return result.Match<ActionResult<IEnumerable<Employee>>>(
                succ =>
                {
                    logger.LogInformation("Successful getting list of all employees.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ});
                },
                fail =>
                {
                    logger.LogWarning("Cannot get list of all employees. Status code 500.");
                    return BadRequest(new ResponseDto() { IsSucceeded = false, Message = "Cannot get list of all employees. Status code 500." });
                });
        }

        [HttpGet("GetEmployeesByPositionId/{Id:int}")]
        public async ValueTask<ActionResult<IEnumerable<Employee>>> GetAllEmployeesByPositionId(int Id)
        {
            var result = await employeesService.GetAllByPositionId(Id);

            return result.Match<ActionResult<IEnumerable<Employee>>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting list of all employees with position id: {Id}.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message});
                });
        }

        [HttpGet("{Id:int}")]
        public async ValueTask<ActionResult<IEnumerable<Employee>>> GetById(int Id)
        {
            var result = await employeesService.GetById(Id);

            return result.Match<ActionResult<IEnumerable<Employee>>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting employee with id: {Id}.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }

        [HttpGet("{email}")]
        public async ValueTask<ActionResult<IEnumerable<Employee>>> GetByName(string email)
        {
            var result = await employeesService.GetByEmail(email);

            return result.Match<ActionResult<IEnumerable<Employee>>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting employee with email: {email}.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateEmployee([FromBody] EmployeeDto employee)
        {
            var result = await employeesService.Create(employee);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Employee with name: {succ.FirstName + " " + succ.LastName} was successful created.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return BadRequest(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }

        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteEmployeeById(int id)
        {
            var result = await employeesService.DeleteById(id);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Employee with id: {id} was successful deleted.");
                    return Ok(new ResponseDto() { IsSucceeded = true, Message = $"Employee with id: {id} was deleted successful." });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }

        [HttpPut("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateEmployee([FromBody] EmployeeDto employee, int id)
        {
            var result = await employeesService.Update(id, employee);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"Employee with id: {succ.EmployeeId} was successful updated");
                    return Ok(new ResponseDto() { IsSucceeded = true, Result = succ });
                },
                fail =>
                {
                    if (fail is ValidationException exception)
                    {
                        logger.LogWarning(exception.Message);
                        return BadRequest(new ResponseDto() { IsSucceeded = false, Message = exception.Message });
                    }

                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }
    }
}
