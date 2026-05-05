using System.Text;
using Data.Entities;
using Infrastructure.Context;
using Infrastructure.Repos.Interfaces;
using Infrastructure.Repos;
using Library_Borrowing_Books.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Configuration;
using Service.Services;
using Infrastructure;

namespace Library_Borrowing_Books.Api
{
    public static class DependencyInjection
    {


        public static IServiceCollection ServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
            services.Configure<AppUrlSettings>(configuration.GetSection(AppUrlSettings.SectionName));
            services.AddSingleton<IEmailNotificationService, SmtpEmailNotificationService>();

            services.AddDataBaseServices(configuration);
            services.AddJwtAuthentication(configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library Borrowing Books API",
                    Version = "v1",
                    Description =
                        "Auth: Register, login, logout, forgot-password, reset-password, confirm-email (GET)."
                });
                options.CustomSchemaIds(type => type.FullName!.Replace('+', '.'));
            });
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
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration["ApiSettings:Secret"]
                ?? throw new InvalidOperationException("ApiSettings:Secret is not configured.");
            var issuer = configuration["ApiSettings:Issuer"] ?? "LibraryBorrowingBooks";
            var audience = configuration["ApiSettings:Audience"] ?? "LibraryBorrowingBooksClients";

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

            return services;
        }
    }
}
