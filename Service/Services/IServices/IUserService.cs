using Data.Entities;
using Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> AssignRoleToUserAsync(string userId, string role);
    }
}
