using AuthAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public async ValueTask<ActionResult<ResponseDto>> Login()
        {
            return Ok();
        }

        public async ValueTask<ActionResult<ResponseDto>> Register()
        {
            return Ok();
        }

        public async ValueTask<ActionResult<ResponseDto>> AssignRole()
        {
            return Ok();
        }

        public async ValueTask<ActionResult<ResponseDto>> ChangePassword()
        {
            return Ok();
        }
    }
}
