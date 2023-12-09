using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PositionsAPI.Models.DTOs;
using PositionsAPI.Models.Entities;
using PositionsAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PositionsAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsService positionsService;
        private readonly ILogger<PositionsController> logger;

        public PositionsController(
            IPositionsService positionsService,
            ILogger<PositionsController> logger)
        {
            this.positionsService = positionsService;
            this.logger = logger;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
            var result = await positionsService.GetAll();

            return result.Match<ActionResult<IEnumerable<Position>>>(
                succ =>
                {
                    logger.LogInformation("Successful getting list of all positions.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning("Cannot get list of all positions. Status code 500.");
                    return BadRequest(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = "Cannot get list of all positions. Status code 500."
                    });
                });
        }

        [HttpGet("GetByDepartmentId/{id:int}")]
        public async ValueTask<ActionResult<IEnumerable<Position>>> GetAllPositionsByDepartmentId(int id)
        {
            var result = await positionsService.GetAllByDepartmentId(id);

            return result.Match<ActionResult<IEnumerable<Position>>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting list of all positions with department id: {id}.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        [HttpGet("{id:int}")]
        public async ValueTask<ActionResult<Position>> GetPositionById(int id)
        {
            var result = await positionsService.GetById(id);

            return result.Match<ActionResult<Position>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting department with id: {id}.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        [HttpGet("{name:alpha}")]
        public async ValueTask<ActionResult<Position>> GetPositionByName(string name)
        {
            var result = await positionsService.GetByName(name);

            return result.Match<ActionResult<Position>>(
                succ =>
                {
                    logger.LogInformation($"Successful getting department with name: {name}.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        [HttpPost]
        public async ValueTask<ActionResult<Position>> CreatePositions(PositionDto positionDto)
        {
            var result = await positionsService.Create(positionDto);

            return result.Match<ActionResult<Position>>(
                succ =>
                {
                    logger.LogInformation($"Position with name: {positionDto.PositionName} was successful created.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return BadRequest(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        [HttpPut("{id:int}")]
        public async ValueTask<ActionResult<Position>> UpdatePositions(PositionDto positionDto, int id)
        {
            var result = await positionsService.Update(positionDto, id);

            return result.Match<ActionResult<Position>>(
                succ =>
                {
                    logger.LogInformation($"Position with name: {positionDto.PositionName} was successful updated.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);

                    if (fail is ValidationException exception)
                    {
                        return BadRequest(new ResponseDto()
                        {
                            IsSucceeded = true,
                            Message = exception.Message
                        });
                    }
                        
                    return NotFound(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Message = fail.Message
                    });
                });
        }

        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<Position>> DeletePositionById(int id)
        {
            var result = await positionsService.DeleteById(id);

            return result.Match<ActionResult<Position>>(
                succ =>
                {
                    logger.LogInformation($"Position with id: {id} was successful deleted.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded= true,
                        Message = $"Position with id: {id} was successful deleted."
                    });
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }
    }
}
