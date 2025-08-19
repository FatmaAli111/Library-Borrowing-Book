using Data.Dtos;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
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

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {

           var validationresult= ValidationService.Validate(loginDTO, new LoginValidator());
            if (validationresult.Result != null)
                return BadRequest(validationresult);

            var result = await authService.LoginAsync(loginDTO);

            return Ok(result);
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully. Please remove your token." });
        }


    }
}
