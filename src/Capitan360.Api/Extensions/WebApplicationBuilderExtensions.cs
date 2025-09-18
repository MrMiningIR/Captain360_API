using Capitan360.Api.Middlewares;
using Capitan360.Application.Features.Permission.Services;
using Capitan360.Domain.Constants;
using Finbuckle.MultiTenant;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using Serilog;

namespace Capitan360.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        #region Swagger

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1", Description = "توضیحات API" });
            options.DocumentFilter<ApiResponseFilter>();

            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        #endregion Swagger

        #region Serilog

        builder.Host.UseSerilog((context, loggerConfiguration) =>
       {
           loggerConfiguration.ReadFrom.Configuration(context.Configuration);
       });

        #endregion Serilog

        #region Middlewares

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        builder.Services.AddScoped<PermissionMiddleware>();
        //  builder.Services.AddScoped<TokenValidationMiddleware>();

        #endregion Middlewares

        #region CORS

        // Add CORS policy
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAll", corsPolicyBuilder =>
        //    {
        //        corsPolicyBuilder.AllowAnyOrigin()
        //               .AllowAnyMethod()
        //               .AllowAnyHeader();
        //    });
        //});

        #endregion CORS

        #region Tenant

        builder.Services.AddMultiTenant<TenantInfo>()
    .WithHeaderStrategy(ConstantNames.IdentifierHeaderName)
    .WithStore<DynamicTenantStore>(ServiceLifetime.Singleton);

        #endregion Tenant 

        builder.Services.AddSingleton<PermissionCollectorService>();

        #region Telemetry

        builder.Services.AddOpenTelemetry()
            .WithMetrics(

                providerBuilder =>
                {
                    providerBuilder.AddAspNetCoreInstrumentation();
                    providerBuilder.AddRuntimeInstrumentation();

                    providerBuilder.AddPrometheusExporter();
                });

        #endregion Telemetry

        return builder;
    }
}