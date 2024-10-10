using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserServer.Core.Configurations;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;
using UserServer.Core.Service;
using UserServer.WebHost.Middleware;
using UserServer.DataAccess.Data;
using UserServer.DataAccess.Repositories;
using UserServer.Core.Mappers;
using UserServer.WebHost.Extensions;
using UserServer.DataAccess.Extensions;

namespace UserServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            await app.AddMigrationAndSeedDataAsync();

            if (app.Environment.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Service API V1"); // Установка конечной точки для Swagger
                    c.RoutePrefix = string.Empty; // Устанавливаем Swagger UI на корень (http://localhost:5000/)
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseIdentityServer();

            await app.AddCustomMiddleware();

            app.UseAuthorization();

            // Настройка конечных точек
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
