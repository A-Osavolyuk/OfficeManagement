using DepartmentsAPI.Models.DTOs;
using DepartmentsAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            this.departmentsService = departmentsService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetAllDepartment()
        {
            var result = await departmentsService.GetAll();

            return result.Match<ActionResult<ResponseDto>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ}),
                fail => StatusCode(500));
        }

        [HttpGet("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> GetDepartmentById(int id)
        {
            var result = await departmentsService.GetById(id);

            return result.Match<ActionResult<ResponseDto>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail => NotFound(fail.Message));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateDepartmnet([FromBody]DepartmentDto department)
        {
            var result = await departmentsService.Create(department);

            return result.Match<ActionResult<ResponseDto>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail => BadRequest(fail.Message));
        }

        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteDepartmentById(int id)
        {
            var result = await departmentsService.DeleteById(id);

            return result.Match<ActionResult<ResponseDto>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Message = $"Department with id: {id} was deleted successful." }),
                fail => NotFound(fail.Message));
        }

        [HttpPut("{id:int}")]
        public async ValueTask<ActionResult<ResponseDto>> CreateDepartment([FromBody] DepartmentDto department, int id)
        {
            var result = await departmentsService.Update(department, id);

            return result.Match<ActionResult<ResponseDto>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail =>
                {
                    if (fail is ValidationException exception) 
                        return BadRequest(exception.Message);
                    return NotFound(fail.Message);
                });
        }
    }
}
