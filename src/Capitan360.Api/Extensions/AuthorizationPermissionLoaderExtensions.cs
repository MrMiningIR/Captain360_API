using Capitan360.Domain.Constants;
using Capitan360.Infrastructure.Authorization.Services;
using Capitan360.Infrastructure.Services.Utilities;

namespace Capitan360.Api.Extensions;

public static class AuthorizationExtensions
{
    public static async Task AddPoliciesFromDatabaseAsync(this IServiceCollection services)
    {
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(60));
        using var scope = services.BuildServiceProvider().CreateScope();
        var utilService = scope.ServiceProvider.GetRequiredService<IUtilsService>();
        bool tableExists = await utilService.CheckTableExistsAsync(ConstantNames.PermissionsTable, cts.Token);
        if (tableExists)
        {
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            await permissionService.LoadPoliciesAsync(services, cts.Token);
        }
        else
        {
            throw new SystemException("Permissions Table is not ready!");
        }
    }
}