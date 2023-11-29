using AuthAPI.Models.DTOs;
using AuthAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ILogger<AuthController> logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        //public async ValueTask<ActionResult<ResponseDto>> Login()
        //{
        //    return Ok();
        //}

        [HttpPost("Register")]
        public async ValueTask<ActionResult<ResponseDto>> Register([FromBody]RegistrationRequestDto registrationRequestDto)
        {
            var result = await authService.Register(registrationRequestDto);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"User with email: {registrationRequestDto.Email} was registered successful.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true, 
                        Message = $"User with email: {registrationRequestDto.Email} was registered successful.", 
                        Result = succ
                    });
                },
                fail =>
                {
                    if (fail is ValidationException exception)
                    {
                        logger.LogWarning($"Validation exception: {exception.Message}");
                        return BadRequest(new ResponseDto()
                        {
                            IsSucceeded = false,
                            Message = exception.Message,
                        });
                    }

                    logger.LogWarning($"Exception with registration: {fail.Message}");
                    return BadRequest(fail.Message);
                });
        }

        //public async ValueTask<ActionResult<ResponseDto>> AssignRole()
        //{
        //    return Ok();
        //}

        //public async ValueTask<ActionResult<ResponseDto>> ChangePassword()
        //{
        //    return Ok();
        //}
    }
}
