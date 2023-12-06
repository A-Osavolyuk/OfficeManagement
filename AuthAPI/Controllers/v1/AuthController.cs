using AuthAPI.Models.DTOs;
using AuthAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ILogger<AuthController> logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async ValueTask<ActionResult<ResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await authService.Login(loginRequestDto);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation($"User with email: {loginRequestDto.Email} successful logged in.");
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Message = $"User with email: {loginRequestDto.Email} successful logged in.",
                        Result = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning($"Exception while logging in user: {fail.Message}", 50);
                    return BadRequest(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        [HttpPost("Register")]
        public async ValueTask<ActionResult<ResponseDto>> Register([FromBody] RegistrationRequestDto registrationRequestDto)
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
                        logger.LogWarning($"Validation exception: {exception.Message}", 50);
                        return BadRequest(new ResponseDto()
                        {
                            IsSucceeded = false,
                            Message = exception.Message,
                        });
                    }

                    logger.LogWarning($"Exception with registration: {fail.Message}", 50);
                    return BadRequest(fail.Message);
                });
        }

        [HttpPost("AssignRole")]
        public async ValueTask<ActionResult<ResponseDto>> AssignRole([FromBody] AssignRoleRequestDto assignRoleRequestDto)
        {
            var result = await authService.AssignRole(assignRoleRequestDto.Email, assignRoleRequestDto.RoleName);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    logger.LogInformation(succ);
                    return Ok(new ResponseDto()
                    {
                        IsSucceeded = true,
                        Message = succ
                    });
                },
                fail =>
                {
                    logger.LogWarning($"Exception while trying assign role: {fail.Message}");
                    return BadRequest(new ResponseDto()
                    {
                        IsSucceeded = false,
                        Message = fail.Message
                    });
                });
        }

        //public async ValueTask<ActionResult<ResponseDto>> ChangePassword()
        //{
        //    return Ok();
        //}
    }
}
