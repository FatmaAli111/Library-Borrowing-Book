using Data.Dtos;
using Microsoft.AspNetCore.Identity;
using Models.DTOs.User;

namespace Service.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(RegisterDto registerDTO);
        Task<LoginResultDto> LoginAsync(LoginDTO loginDTO);
        Task SendForgotPasswordAsync(ForgotPasswordDto dto);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
