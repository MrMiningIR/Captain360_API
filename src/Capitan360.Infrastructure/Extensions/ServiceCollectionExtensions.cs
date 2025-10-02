using Capitan360.Application.Features.Identities.Identities.CustomIdentityErrorDescriber;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;
using Capitan360.Domain.Interfaces.Repositories.ContentTypes;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Capitan360.Infrastructure.Authorization.Requirements;
using Capitan360.Infrastructure.Authorization.Services;
using Capitan360.Infrastructure.Persistence;
using Capitan360.Infrastructure.Repositories.Addresses;
using Capitan360.Infrastructure.Repositories.Companies;
using Capitan360.Infrastructure.Repositories.CompanyDomesticPaths;
using Capitan360.Infrastructure.Repositories.CompanyInsurances;
using Capitan360.Infrastructure.Repositories.ContentTypes;
using Capitan360.Infrastructure.Repositories.Identities;
using Capitan360.Infrastructure.Repositories.PackageTypes;
using Capitan360.Infrastructure.Seeders;
using Capitan360.Infrastructure.Services;
using Capitan360.Infrastructure.Services.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

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
                                 throw new Exception("ConString"), x => x.UseNetTopologySuite())
                .EnableSensitiveDataLogging();
        });




        // Identity Configuration
        service.AddIdentity<User, Capitan360.Domain.Entities.Identities.Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lock Setting
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriberMessage>(); ;
        //  .AddUserValidator<CustomUserValidator>();
        // Add User Custom Validator





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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["Jwt:Key"] ?? throw new NullReferenceException("تنظیماتی در سیستم ست نشده است")))
            };
            //options.Events = new JwtBearerEvents
            //{
            //    OnMessageReceived = context =>
            //    {
            //        var encryptedToken = context.Request.Cookies["AccessToken"];
            //        if (!string.IsNullOrEmpty(encryptedToken))
            //        {
            //            var encryptionService = context.HttpContext.RequestServices.GetRequiredService<EncryptionService>();
            //            context.Token = encryptionService.Decrypt(encryptedToken);
            //        }
            //        return Task.CompletedTask;
            //    }
            //};
        });


        #region Comment
        // Authorization Policies
        //service.AddAuthorization(options =>
        //{

        //    foreach (var permission in Enum.GetValues(typeof(Permissions)))
        //    {
        //        options.AddPolicy(permission.ToString(), policy => policy.RequireClaim("Permission", permission.ToString()));
        //    }
        //}); 

        //var authorizationBuilder = service.AddAuthorizationBuilder();

        //foreach (var permission in Enum.GetValues(typeof(Permissions)))
        //{
        //    authorizationBuilder.AddPolicy(permission.ToString(), policy =>
        //        policy.RequireClaim("Permission", permission.ToString()));
        //}
        #endregion






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
        service.AddScoped<ITokenRepository, TokenRepository>();
        service.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();

        service.AddScoped<ICompanyRepository, CompanyRepository>();
        service.AddScoped<IAddressRepository, AddressRepository>();
        service.AddScoped<ICompanyAddressRepository, CompanyAddressRepository>();
        service.AddScoped<ICompanyUriRepository, CompanyUriRepository>();
        service.AddScoped<ICompanySmsPatternsRepository, CompanySmsPatternsRepository>();
        service.AddScoped<ICompanyPreferencesRepository, CompanyPreferencesRepository>();
        service.AddScoped<ICompanyCommissionsRepository, CompanyCommissionsRepository>();
        service.AddScoped<IContentTypeRepository, ContentTypeRepository>();
        service.AddScoped<IPackageTypeRepository, PackageTypeRepository>();
        service.AddScoped<ICompanyDomesticPathRepository, CompanyDomesticPathRepository>();
        service.AddScoped<ICompanyDomesticPathReceiverCompanyRepository, CompanyDomesticPathReceiverCompanyRepository>();
        service.AddScoped<ICompanyDomesticPathStructPricesRepository, CompanyDomesticPathStructPricesRepository>();
        service.AddScoped<ICompanyDomesticPathStructPriceMunicipalAreasRepository, CompanyDomesticPathStructPriceMunicipalAreasRepository>();
        service.AddScoped<ICompanyDomesticPathChargeRepository, CompanyDomesticPathChargeRepository>();
        service.AddScoped<ICompanyDomesticPathChargeContentTypeRepository, CompanyDomesticPathChargeContentTypeRepository>();
        service.AddScoped<ICompanyInsuranceRepository, CompanyInsuranceRepository>();
        service.AddScoped<ICompanyInsuranceChargeRepository, CompanyInsuranceChargeRepository>();
        service.AddScoped<ICompanyInsuranceChargePaymentRepository, CompanyInsuranceChargePaymentRepository>();
        service.AddScoped<ICompanyInsuranceChargePaymentContentTypeRepository, CompanyInsuranceChargePaymentContentTypeRepository>();
        service.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
        service.AddScoped<IUserPermissionVersionControlRepository, UserPermissionVersionControlRepository>();
        service.AddScoped<IUserProfileRepository, UserProfileRepository>();
        service.AddScoped<ICompanyContentTypeRepository, CompanyContentTypeRepository>();
        service.AddScoped<IUtilsService, UtilsService>();
        service.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
        service.AddScoped<ICompanyPackageTypeRepository, CompanyPackageTypeRepository>();

        // service.AddSingleton<IResponseCacheService, ResponseCacheService>();


        service.AddScoped<EncryptionService>();
        service.AddScoped<IAreaRepository, AreaRepository>();

        // Registering Seeders

        service.AddScoped<IPrimaryInformationSeeder, PrimaryInformationSeeder>();


        // 1. پیکربندی Redis
        //service.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration = configurationManager.GetConnectionString("Redis")  ?? throw new Exception("Cannot get redis connection string");;
        //    options.InstanceName = "Cap_";
        //    options.ConfigurationOptions = new ConfigurationOptions
        //    {
        //        ConnectTimeout = 5000,
        //        SyncTimeout = 1000,
        //        AbortOnConnectFail = false
        //    };
        //});
        var redisConnectioan = configurationManager.GetConnectionString("Redis");
        Console.WriteLine($"Redis Connection: {redisConnectioan}");
        service.AddStackExchangeRedisCache(options =>

{
    var redisConnection = configurationManager.GetConnectionString("Redis") ?? throw new Exception("Cannot get redis connection string");
    options.Configuration = redisConnection;
    options.InstanceName = ConstantNames.CachePrefix;
    options.ConfigurationOptions = new ConfigurationOptions
    {
        EndPoints = { redisConnection.Split(',')[0] }, // استخراج آدرس (مثلاً localhost:6379)
        ConnectTimeout = 5000,// حداکثر زمانی که StackExchange.Redis برای برقراری اتصال به سرور Redis صبر می‌کنه. 
        SyncTimeout = 1000, //حداکثر زمانی که StackExchange.Redis برای انجام عملیات همزمان (مثل خواندن یا نوشتن داده در Redis) صبر می‌کنه.
        // نکته: عملیات غیرهمزمان (async) مثل GetOrCreateAsync تحت تأثیر SyncTimeout نیستن، چون از مدل async استفاده می‌کنن.
        AbortOnConnectFail = false,
        Ssl = redisConnection.Contains("ssl=True", StringComparison.OrdinalIgnoreCase),

    };
});

        // 2. پیکربندی HybridCache
        service.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024; // 1MB
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(20), // انقضای Redis
                                                       // Expiration = TimeSpan.FromSeconds(50), // انقضای Redis
                LocalCacheExpiration = TimeSpan.FromMinutes(10) // انقضای MemoryCache L1

                // LocalCacheExpiration = TimeSpan.FromSeconds(30)
                //  ,Flags = HybridCacheEntryFlags.DisableLocalCache | HybridCacheEntryFlags.DisableCompression

            };
        });



    }
}