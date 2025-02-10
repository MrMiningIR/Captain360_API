using Capitan360.Infrastructure.Authorization.Services;

namespace Capitan360.Api.Extensions;

public static class AuthorizationExtensions
{
    public static async Task AddPoliciesFromDatabaseAsync(this IServiceCollection services)
    {

        using var scope = services.BuildServiceProvider().CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        await permissionService.LoadPoliciesAsync(services);
    }
}