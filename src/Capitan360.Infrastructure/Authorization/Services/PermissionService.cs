using Capitan360.Infrastructure.Authorization.Requirements;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Infrastructure.Authorization.Services;

public class PermissionService(ApplicationDbContext dbContext) : IPermissionService
{
    //Todo: Needs to be optimized and refactored by using Repository Pattern

    public async Task<bool> HasPermissionAsync(string userId, string permission)
    {

        var hasUserPermission = await dbContext.UserPermissions
            .AnyAsync(up => up.UserId == userId && up.Permission.Name == permission);



        return hasUserPermission;


        //var allPermission = await GetUserPermissionsAsync(userId);
        //return allPermission.Contains(permission);
    }

    public async Task<bool> HasAnyPermissionAsync(string userId)
    {


        var hasUserPermission = await dbContext.UserPermissions.AnyAsync(up => up.UserId == userId);
        Console.WriteLine($"UserPermissions for {userId}: {hasUserPermission}");

        var hasRolePermission = await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)

            .AnyAsync();
        Console.WriteLine($"RolePermissions for {userId}: {hasRolePermission}");




        return hasUserPermission || hasRolePermission;
    }


    //public async Task<bool> HasPermissionAsync(string userId)
    //{
    //    var hasPermission = await dbContext.UserPermissions
    //                            .AnyAsync(up => up.UserId == userId)
    //                        || await dbContext.RolePermissions
    //                            .AnyAsync(rp => dbContext.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == rp.RoleId))
    //                        || await dbContext.GroupPermissions
    //                            .AnyAsync(gp => dbContext.UserGroups.Any(ug => ug.UserId == userId && ug.GroupId == gp.GroupId));



    //    return hasPermission;
    //}
    //public async Task<bool> HasAnyPermissionAsync(string userId)
    //{
    //    // چک کردن UserPermissions
    //    var hasUserPermission = await dbContext.UserPermissions
    //        .AnyAsync(up => up.UserId == userId);

    //    // چک کردن RolePermissions
    //    var hasRolePermission = await dbContext.UserRoles
    //        .Where(ur => ur.UserId == userId)
    //        .Join(dbContext.RolePermissions,
    //            ur => ur.RoleId,
    //            rp => rp.RoleId,
    //            (ur, rp) => rp)
    //        .AnyAsync();

    //    // چک کردن GroupPermissions
    //    var hasGroupPermission = await dbContext.UserGroups
    //        .Where(ug => ug.UserId == userId)
    //        .Join(dbContext.GroupPermissions,
    //            ug => ug.GroupId,
    //            gp => gp.GroupId,
    //            (ug, gp) => gp)
    //        .AnyAsync();

    //    return hasUserPermission || hasRolePermission || hasGroupPermission;
    //}


    #region Comment
    //public async Task<bool> HasPermissionAsync(string userId, string permission)
    //{
    //    // Get user's roles
    //    var userRoles = await context.UserRoles
    //        .Where(ur => ur.UserId == userId)
    //        .Select(ur => ur.RoleId)
    //        .ToListAsync();

    //    if (userRoles.Count == 0)
    //        return false;

    //    // Get permissions from roles
    //    var rolePermissions = await context.RolePermissions
    //        .Where(rp => userRoles.Contains(rp.RoleId))
    //        .Include(rp => rp.Permission)
    //        .ToListAsync();

    //    // Get groups associated with roles
    //    var roleGroups = await context.RoleGroups
    //        .Where(rg => userRoles.Contains(rg.RoleId))
    //        .Select(rg => rg.GroupId)
    //        .ToListAsync();

    //    // Get permissions from groups
    //    var groupPermissions = await context.GroupPermissions
    //        .Where(gp => roleGroups.Contains(gp.GroupId))
    //        .Include(gp => gp.Permission)
    //        .ToListAsync();

    //    // Get permissions from roles
    //    var rolePermissionNames = rolePermissions
    //        .Select(rp => rp.Permission.Name); // Extract permission names from rolePermissions

    //    // Get permissions from groups
    //    var groupPermissionNames = groupPermissions
    //        .Select(gp => gp.Permission.Name); // Extract permission names from groupPermissions

    //    // Combine all permissions and remove duplicates
    //    var allPermissions = rolePermissionNames
    //        .Concat(groupPermissionNames) // Combine the two lists
    //        .Distinct(); // Remove duplicates




    //    // Check if the required permission exists
    //    return allPermissions.Contains(permission);
    //} 
    #endregion
    public async Task LoadPoliciesAsync(IServiceCollection services, CancellationToken cancellationToken)
    {

        if (await dbContext.Database.CanConnectAsync(cancellationToken) &&
            await dbContext.Permissions.AnyAsync(cancellationToken))
        {

            var permissions = await dbContext.Permissions.ToListAsync(cancellationToken);

            var authorizationBuilder = services.AddAuthorizationBuilder();
            foreach (var permission in permissions)
            {
                //authorizationBuilder
                //    .AddPolicy(permission.Name, policy =>
                //        policy.RequireClaim("Permission", permission.Name));
                authorizationBuilder
                    .AddPolicy(permission.Name, policy =>
                        policy.Requirements.Add(new PermissionRequirement(permission.Name)));
            }
        }
    }

    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        // 1.Getting UserPermission
        var userPermissionNames = await dbContext.UserPermissions
            .Where(up => up.UserId == userId)
            .Select(up => up.Permission.Name)
            .ToListAsync();

        // 2. GettingRoles (Role)
        var userRoles = await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        // 3. Getting RolePermission


        // 4. Getting  UserGroups


        // 5. Getting GroupPermission


        // 6. Combine all permissions and remove duplicates
        var allPermissions = userPermissionNames
        .Distinct()
            .ToList();

        return allPermissions;
    }
}


//Alternative!

//public async Task<bool> HasPermissionAsync(string userId, string permission)
//{
//    var hasPermission = await _dbContext.UserPermissions
//        .Where(up => up.UserId == userId && up.Permission.Name == permission)
//        .Union(_dbContext.RolePermissions
//            .Where(rp => _dbContext.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == rp.RoleId))
//            .Where(rp => rp.Permission.Name == permission))
//        .Union(_dbContext.GroupPermissions
//            .Where(gp => _dbContext.UserGroups.Any(ug => ug.UserId == userId && ug.GroupId == gp.GroupId))
//            .Where(gp => gp.Permission.Name == permission))
//        .AnyAsync();

//    return hasPermission;
//}