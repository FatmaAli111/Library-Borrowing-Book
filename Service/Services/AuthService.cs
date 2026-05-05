using System.Net;
using Data.Dtos;
using Data.Entities;
using DataAcess.Repos.IRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models.DTOs.Auth;
using Models.DTOs.User;
using Service.Configuration;
using Service.Services;
using Service.Services.IServices;

namespace Service.IServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailNotificationService emailSender;
        private readonly EmailSettings emailSettings;
        private readonly AppUrlSettings urlSettings;

        public AuthService(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailNotificationService emailSender,
            IOptions<EmailSettings> emailOptions,
            IOptions<AppUrlSettings> urlOptions)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.emailSender = emailSender;
            emailSettings = emailOptions.Value;
            urlSettings = urlOptions.Value;
        }

        public async Task<UserDTO> RegisterAsync(RegisterDto registerDTO)
        {
            var emailExist = await userRepository.GetUserByEmailAsync(registerDTO);
            if (emailExist != null)
            {
                return new UserDTO
                {
                    ErrorMessages = new List<string> { "Email already exists." }
                };
            }

            var result = await userRepository.Register(registerDTO);

            if (result.ErrorMessages.Count == 0
                && emailSettings.SendConfirmationOnRegister
                && !string.IsNullOrEmpty(result.Id))
            {
                var user = await userManager.FindByIdAsync(result.Id);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var apiBase = urlSettings.PublicApiUrl.TrimEnd('/');
                    var link =
                        $"{apiBase}/api/Auth/confirm-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token)}";

                    await emailSender.SendEmailAsync(
                        user.Email,
                        "Confirm your library account",
                        $"Confirm your email by opening this link:\n{link}",
                        $"<p>Please <a href=\"{WebUtility.HtmlEncode(link)}\">confirm your email</a> to activate your account.</p>");
                }
            }

            return result;
        }

        public Task<LoginResultDto> LoginAsync(LoginDTO loginRequestDTO)
        {
            return userRepository.Login(loginRequestDTO);
        }

        public async Task SendForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var clientBase = urlSettings.ClientAppUrl.TrimEnd('/');
            var link =
                $"{clientBase}/reset-password?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(token)}";

            await emailSender.SendEmailAsync(
                user.Email!,
                "Reset your password",
                $"You requested a password reset. Open this link to set a new password (or copy the URL into your app):\n{link}",
                $"<p>Reset your password using <a href=\"{WebUtility.HtmlEncode(link)}\">this link</a>.</p>");
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid request." });

            return await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid confirmation link." });

            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
