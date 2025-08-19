using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.IServices;

namespace Library_Borrowing_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize(Roles = "Admin")]

    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return result ? Ok("User deleted") : NotFound("User not found");
        }

        [HttpPost("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(string id, [FromBody] string role)
        {
            var result = await _userService.AssignRoleToUserAsync(id, role);

            return result ? Ok("Role assigned") : BadRequest("Failed to assign role");
        }

    }
}
