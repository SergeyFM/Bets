using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserServer.Core.Configurations;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;
using UserServer.Core.Mappers;
using UserServer.Core.Service;
using UserServer.DataAccess.Data;
using UserServer.DataAccess.Extensions;
using UserServer.DataAccess.Repositories;

namespace UserServer.WebHost.Extensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataSetvices(configuration);

            #region Dependency

            services.Configure<JwtSettings>
               (configuration.GetSection("Jwt"));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped<JwtUtils>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ReportApiVersions = true;
            });

            services.AddAutoMapper(typeof(UserProfile));

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Service API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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

            #endregion

            #region IdentityServer

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
                .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
                .AddInMemoryClients(IdentityServerConfig.Clients)
                .AddAspNetIdentity<User>();


            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddAuthentication(option =>
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

            #endregion

            return services;
        }
    }
}
