using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Infrastructure.Authorization.Services;

internal class PermissionService(ApplicationDbContext context) : IPermissionService
{
    public async Task<bool> HasPermissionAsync(string userId, string permission)
    {
        // Get user's roles
        var userRoles = await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        if (userRoles.Count == 0)
            return false;

        // Get permissions from roles
        var rolePermissions = await context.RolePermissions
            .Where(rp => userRoles.Contains(rp.RoleId))
            .Include(rp => rp.Permission)
            .ToListAsync();

        // Get groups associated with roles
        var roleGroups = await context.RoleGroups
            .Where(rg => userRoles.Contains(rg.RoleId))
            .Select(rg => rg.GroupId)
            .ToListAsync();

        // Get permissions from groups
        var groupPermissions = await context.GroupPermissions
            .Where(gp => roleGroups.Contains(gp.GroupId))
            .Include(gp => gp.Permission)
            .ToListAsync();

        // Get permissions from roles
        var rolePermissionNames = rolePermissions
            .Select(rp => rp.Permission.Name); // Extract permission names from rolePermissions

        // Get permissions from groups
        var groupPermissionNames = groupPermissions
            .Select(gp => gp.Permission.Name); // Extract permission names from groupPermissions

        // Combine all permissions and remove duplicates
        var allPermissions = rolePermissionNames
            .Concat(groupPermissionNames) // Combine the two lists
            .Distinct(); // Remove duplicates




        // Check if the required permission exists
        return allPermissions.Contains(permission);
    }
    public async Task LoadPoliciesAsync(IServiceCollection services ,CancellationToken cancellationToken)
    {
        if (await context.Database.CanConnectAsync(cancellationToken) && 
            await context.Permissions.AnyAsync(cancellationToken: cancellationToken))
        {

            var permissions = await context.Permissions.ToListAsync(cancellationToken: cancellationToken);

            var authorizationBuilder = services.AddAuthorizationBuilder();
            foreach (var permission in permissions)
            {
                authorizationBuilder
                    .AddPolicy(permission.Name, policy =>
                        policy.RequireClaim("Permission", permission.Name));
            }
        }
    }
}
