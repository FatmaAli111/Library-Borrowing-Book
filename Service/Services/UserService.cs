using Core.Results.Application.Common;
using Data.Entities;
using DataAcess.Repos;
using Infrastructure;
using Models.DTOs.User;
using Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
        }

        public Task<bool> AssignRoleToUserAsync(string userId, string role)
        {

            var result = unitOfWork.UserRepository.AssignRoleToUserAsync(userId, role);
            
            return result;

        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var result = await unitOfWork.UserRepository.DeleteUserAsync(userId);
   
            return result;

        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var result =await unitOfWork.UserRepository.GetAllUsersAsync();
            var users = result.Select(x => new UserDTO
            {
                Email = x.Email,
                UserName = x.UserName,
                Name = x.FirstName + x.LastName,

            }).ToList();
            return users;
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            var result = await unitOfWork.UserRepository.GetUserByID(userId);
            var user= new UserDTO
            {
                Email = result.Email,
                UserName = result.UserName,
                Name = result.FirstName + result.LastName,

            };
            return user;

        }
    }
}
