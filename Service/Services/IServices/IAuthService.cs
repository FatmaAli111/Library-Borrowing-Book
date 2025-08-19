using Azure;
using Data.Dtos;
using Models.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDTO);
        Task<LoginResponseDTO> LoginAsync(LoginDTO LoginDTO);


    }
}
