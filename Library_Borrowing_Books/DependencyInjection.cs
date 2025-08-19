using Data.Entities;
using Infrastructure.Context;
using Infrastructure.Repos.Interfaces;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Library_Borrowing_Books.Api
{
    public static class DependencyInjection
    {


        public static IServiceCollection ServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDataBaseServices(configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddOpenApi();
            services.AddAuthorization();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

 

            return services;
        }


        public static IServiceCollection AddDataBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>() 
            .AddDefaultTokenProviders(); ;

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));




            //add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            //add repos
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
