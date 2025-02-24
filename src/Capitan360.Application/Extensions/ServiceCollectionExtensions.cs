
using Capitan360.Application.Services.Identity;
using Capitan360.Application.Services.Identity.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        service.AddScoped<IIdentityService, IdentityService>();


        // Register IHttpContextAccessor
        service.AddHttpContextAccessor();

        // Register other services
        service.AddScoped<IUserContext, UserContext>();


        service.AddAutoMapper(applicationAssembly);

        service.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}

