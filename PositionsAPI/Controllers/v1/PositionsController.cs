using Microsoft.AspNetCore.Http;
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning("Cannot get list of all positions. Status code 500.");
                    return StatusCode(500);
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return BadRequest(fail.Message);
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
                    return Ok(succ);
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    
                    if (fail is ValidationException exception)
                        return BadRequest(exception.Message);
                    return NotFound(fail.Message);
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
                    return Ok($"Position with id: {id} was successful deleted.");
                },
                fail =>
                {
                    logger.LogWarning(fail.Message);
                    return NotFound(fail.Message);
                });
        }
    }
}
