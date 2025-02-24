using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Constants;
using Capitan360.Infrastructure.Authorization.Requirements;
using Capitan360.Infrastructure.Authorization.Services;
using Capitan360.Infrastructure.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authorization;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.Identity;
using Capitan360.Infrastructure.Repositories.Identity;
using Capitan360.Infrastructure.Seeders;

namespace Capitan360.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructure(this IServiceCollection service,
        IConfigurationManager configurationManager)

    {
        // Add DbContext

        service.AddDbContext<ApplicationDbContext>(options =>
        {

            options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection") ??
                                 throw new Exception("ConString"),x=>x.UseNetTopologySuite())
                .EnableSensitiveDataLogging();
        });




        // Identity Configuration
        service.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // JWT Authentication Configuration
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configurationManager["Jwt:Issuer"],
                ValidAudience = configurationManager["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["Jwt:Key"]))
            };
        });

       






        service.AddScoped<IPermissionService, PermissionService>();
        service.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        service.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());


        // registering repositories
        service.AddScoped<IPermissionRepository, PermissionRepository>();
        service.AddScoped<ITokenBlacklistsRepository, TokenBlacklistsRepository>();
        service.AddScoped<IIdentityRepository, IdentityRepository>();
        service.AddScoped<IGroupRepository, GroupRepository>();
        service.AddScoped<IUserGroupRepository, UserGroupRepository>();
        service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        service.AddScoped<ITokenService, TokenService>();

        // Registering Seeders

        service.AddScoped<IPrimaryInformationSeeder, PrimaryInformationSeeder>();





    }
}