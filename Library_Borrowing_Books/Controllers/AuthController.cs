using Data.Dtos;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.Services.IServices;

namespace Library_Borrowing_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validationresult = ValidationService.Validate(registerDto, new RegisterValidator());

            if (validationresult.Result != null)
                return BadRequest(validationresult);

            var result = await authService.RegisterAsync(registerDto);
            if (result.ErrorMessages.Count > 0)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var validationresult = ValidationService.Validate(loginDTO, new LoginValidator());
            if (validationresult.Result != null)
                return BadRequest(validationresult);

            var result = await authService.LoginAsync(loginDTO);
            if (result.Success != null)
                return Ok(result.Success);

            if (result.Failure == LoginFailureKind.EmailNotConfirmed)
                return StatusCode(StatusCodes.Status403Forbidden,
                    new { message = "Please confirm your email before signing in." });

            return Unauthorized(new { message = "Invalid email or password." });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully. Please remove your token." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var validationresult = ValidationService.Validate(dto, new ForgotPasswordValidator());
            if (validationresult.Result != null)
                return BadRequest(validationresult);

            await authService.SendForgotPasswordAsync(dto);
            return Ok(new
            {
                message = "If your email is registered, you will receive password reset instructions shortly."
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var validationresult = ValidationService.Validate(dto, new ResetPasswordValidator());
            if (validationresult.Result != null)
                return BadRequest(validationresult);

            var result = await authService.ResetPasswordAsync(dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = "Your password has been reset. You can sign in now." });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return BadRequest(new { message = "Invalid confirmation link." });

            var result = await authService.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = "Thank you — your email is confirmed. You can sign in now." });
        }
    }
}
