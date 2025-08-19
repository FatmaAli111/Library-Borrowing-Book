using Data.Dtos;
using Data.Entities;
using Infrastructure.Repos.Interfaces;
using Models.DTOs.Auth;
using Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repos.IRepos
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<bool> IsUniqueUserName(string username);
        Task<LoginResponseDTO> Login(LoginDTO loginDTO);
        Task<UserDTO> Register(RegisterDto registerDTO);
        Task<ApplicationUser> GetUserByID(string userID);
        Task<ApplicationUser> GetUserByEmailAsync(RegisterDto registerDto) ;
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> AssignRoleToUserAsync(string userId, string role);
    }
}