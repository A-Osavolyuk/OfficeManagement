using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PositionsAPI.Models.Entities;

namespace PositionsAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
            return Ok();
        }
    }
}
