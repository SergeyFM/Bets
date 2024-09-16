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

namespace UserServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            if (String.IsNullOrEmpty(connection)) { throw new NullReferenceException(nameof(connection)); }
            connection = string.Format(connection,
                Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE"),
                Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER"),
                Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"));

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(connection)
                );

            builder.Services.Configure<JwtSettings>
                (builder.Configuration.GetSection("Jwt"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Service API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your token in the format **Bearer {your token}**",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { } 
                    }
                });
                c.DocInclusionPredicate((vercion, apiDescripstion) =>
                {
                    var version = apiDescripstion.ActionDescriptor.EndpointMetadata
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(atr => atr.Versions)
                        .Select(v => v.ToString())
                        .ToList();

                    return vercion.Any(v => $"v{v}" == vercion);
                });
            });

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
                .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
                .AddInMemoryClients(IdentityServerConfig.Clients)
                .AddAspNetIdentity<User>();


            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            builder.Services.AddScoped<JwtUtils>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ReportApiVersions = true;
            });

            builder.Services.AddAutoMapper(typeof(UserProfile));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) 
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetService<ApplicationDbContext>();
                    await context.Database.MigrateAsync();
                    await SeedDatabBase(service,context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }

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
            //app.UseMiddleware<JwtMiddleware>();
            app.Use(async (context, next) =>
            {
                var jwtUtil = context.RequestServices.GetRequiredService<JwtUtils>();
                var middlevare = new JwtMiddleware(next, jwtUtil);

                await middlevare.Invoke(context);
            });

            app.UseAuthorization();

            // Настройка конечных точек
            app.MapControllers();

            await app.RunAsync();
        }

        private static async Task SeedDatabBase(IServiceProvider services, ApplicationDbContext context)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();

            // Пример загрузки ролей
            if (!(await roleManager.Roles.AnyAsync()))
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "User" },
                    new Role { Name = "Moderator" }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // Пример загрузки пользователей
            if (!(await userManager.Users.AnyAsync()))
            {
                var adminUser = new User
                {
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Main"
                };

                var result = await userManager.CreateAsync(adminUser, "Qwerty202$");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    Console.WriteLine($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

        }
    }
}
