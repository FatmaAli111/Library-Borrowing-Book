using DataAcess.Repos.IRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Infrastructure.Repos;
using Infrastructure.Context;
using Data.Dtos;
using Models.DTOs.Auth;
using Models.DTOs.User;
using Microsoft.EntityFrameworkCore;


namespace DataAcess.Repos
{
    public class userRepository :GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public userRepository(
            AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration) : base(db)
        {
            this.context = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrWhiteSpace(role))
                return false;

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new ApplicationRole(role));

            var result = await userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }


        public async Task<ApplicationUser> GetUserByEmailAsync(RegisterDto registerDto)
        {

            return await userManager.FindByEmailAsync(registerDto.Email);
        }

        public async Task<ApplicationUser> GetUserByID(string userID)
        {
            var user = await context.User.FindAsync(userID);
            return user ?? throw new InvalidOperationException("User not found.");
        }

        public async Task<bool> IsUniqueUserName(string username)
        {
            var matchUsername = await userManager.FindByNameAsync(username);
            return matchUsername == null;
        }

        public async Task<LoginResultDto> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return new LoginResultDto { Failure = LoginFailureKind.InvalidCredentials };
            }

            var requireConfirm = configuration.GetValue("EmailSettings:RequireConfirmationForLogin", true);
            if (requireConfirm && !user.EmailConfirmed)
            {
                return new LoginResultDto { Failure = LoginFailureKind.EmailNotConfirmed };
            }

            var response = await BuildLoginResponseAsync(user);
            return new LoginResultDto
            {
                Success = response,
                Failure = LoginFailureKind.None
            };
        }

        private async Task<LoginResponseDTO> BuildLoginResponseAsync(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var issuer = configuration["ApiSettings:Issuer"] ?? "LibraryBorrowingBooks";
            var audience = configuration["ApiSettings:Audience"] ?? "LibraryBorrowingBooksClients";
            var secret = configuration["ApiSettings:Secret"]
                ?? throw new InvalidOperationException("ApiSettings:Secret is not configured.");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? user.Id)
            };
            claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);
            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                User = new UserDTO
                {
                    UserName = user.UserName,
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email ?? string.Empty
                }
            };
        }

        public async Task<UserDTO> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Address = registerDto.Address,
                MembershipType = registerDto.MembershipType,
                MembershipStatus = registerDto.MembershipStatus,
                NormalizedEmail = registerDto.Email.ToUpper()
            };

            var userDTO = new UserDTO();

            try
            {
                var result = await userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                   
                    userDTO = new UserDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Name = $"{user.FirstName} {user.LastName}",
                        Email = user.Email ?? string.Empty
                    };

                    var GetUser = await userManager.FindByEmailAsync(registerDto.Email);

                    if (!await roleManager.RoleExistsAsync("Member"))
                        await roleManager.CreateAsync(new ApplicationRole("Member"));

                    if (GetUser != null)
                        await userManager.AddToRoleAsync(GetUser, "Member");
                }
                else
                {
                    userDTO.ErrorMessages = result.Errors.Select(e => e.Description).ToList();
                }
            }
            catch (Exception)
            {
                userDTO.ErrorMessages = new List<string> { "An unexpected error occurred while registering the user." };
            }
           

            return userDTO;
        }


    }
}