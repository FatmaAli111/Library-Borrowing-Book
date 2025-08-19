
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repos.Interfaces;
using Infrastructure.Repos;
using Library_Borrowing_Books.Api;
using Microsoft.EntityFrameworkCore;

using System;
using Service;
using DataAcess.Repos.IRepos;
using DataAcess.Repos;

namespace Library_Borrowing_Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           //Add Module Dependencies
            builder.Services.ServiceCollection(builder.Configuration).AddServicesDependencies();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUserRepository, userRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }
            }
          


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
