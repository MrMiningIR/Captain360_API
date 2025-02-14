
using Capitan360.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {





        // Register IHttpContextAccessor
        service.AddHttpContextAccessor();

        // Register other services
        service.AddScoped<IUserContext, UserContext>();
    }
}

