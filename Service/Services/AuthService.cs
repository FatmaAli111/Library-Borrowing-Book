using Azure.Core;
using Data.Dtos;
using Data.Entities;
using DataAcess.Repos;
using DataAcess.Repos.IRepos;
using Microsoft.AspNetCore.Identity;
using Models.DTOs.Auth;
using Service.Services.IServices;

namespace Service.IServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager <ApplicationUser> userManager;
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository UserRepository)
        {
          userRepository = UserRepository;
        }



        public async Task<string> RegisterAsync(RegisterDto registerDTO)
        {
            var emailExist = await userRepository.GetUserByEmailAsync(registerDTO);
           
            if (emailExist != null)
            {
                return "Email Already Exist";
            }

             await userRepository.Register(registerDTO);
            return "Registered Successfully";
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginRequestDTO)
        {
            return await userRepository.Login(loginRequestDTO);
        }

    }
}
