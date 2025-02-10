using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Infrastructure.Authorization.Services;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(string userId, string permission);

    Task LoadPoliciesAsync(IServiceCollection services ,  CancellationToken cancellationToken = default);
}