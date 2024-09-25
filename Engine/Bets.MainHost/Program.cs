using System;
using Bets.MainHost.Config.ini;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bets.MainHost;

public class Program {
    public static void Main(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Kestrel config
        builder.ConfigureKestrelOptions();

        // Get config
        IConfiguration configuration = builder.ReadAppSettings();
        configuration.PrintAllConfigurations();

        // Serilog
        builder.AddLogService();
        Log.Information("Bets.Engine is launching...");

        // Add and init options and services
        IServiceCollection services = builder.Services;
        services
            .AddConfOptions()
            .AddServices()
            .AddControllersWithViews();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Main/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Main}/{action=Index}/{id?}");

        try {
            app.Run();
        }
        catch (Exception ex) {
            Log.Error(ex, "An unhandled exception occurred during application execution.");
            throw; // Re-throw the exception to allow it to propagate
        }
        finally {
            Log.Information("Application stopped.");
            Log.CloseAndFlush();
        }
    }
}
