using Microsoft.Extensions.DependencyInjection;
using Service.IServices;
using Service.Services;
using Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBorrowBookService, BorrowBookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
