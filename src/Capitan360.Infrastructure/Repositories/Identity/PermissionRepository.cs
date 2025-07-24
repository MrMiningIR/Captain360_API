using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Dtos;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identity;

internal class PermissionRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPermissionRepository
{


    // Permission
    public async Task<IReadOnlyList<Permission>> GetAllPolicy(CancellationToken cancellationToken)
    {
        return await dbContext.Permissions.ToListAsync(cancellationToken);
    }
    public async Task<List<int>> GetAllPermissionsId(CancellationToken cancellationToken)
    {
        return await dbContext.Permissions.AsNoTracking().Select(x => x.Id).ToListAsync(cancellationToken);
    }

    public async Task<List<ParentPermissionTransfer>> GetParentPermissions(CancellationToken ct)
    {
        return await dbContext.Permissions.AsNoTracking().Select(x =>
                new ParentPermissionTransfer { Parent = x.Parent, ParentDisplayName = x.ParentDisplayName, ParentCode = x.ParentCode }).Distinct()
            .ToListAsync(ct);
    }

    public async Task<List<Permission>> GetPermissionsByParentName(string parent, CancellationToken ct)
    {
        return await dbContext.Permissions.AsNoTracking().Where(x => x.Parent.ToLower() == parent.ToLower()).Distinct().ToListAsync(ct);
    }

    public async Task<List<Permission>> GetPermissionsByParentCode(Guid parentCode, CancellationToken ct)
    {
        return await dbContext.Permissions.AsNoTracking().Where(x => x.ParentCode == parentCode).Distinct().ToListAsync(ct);
    }

    public async Task<List<Permission>> GetFullListPermission(CancellationToken ct)
    {
        return await dbContext.Permissions.AsNoTracking().Distinct().ToListAsync(ct);
    }


    public Task<List<Permission>> GetPermissionsByParentId(int parentQueryParentId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<(IReadOnlyList<Permission>, int)> GetAllMatchingPermissions(string? searchPhrase,
        int pageSize, int pageNumber,
        CancellationToken ct)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Permissions.AsNoTracking()

            .Where(r => searchPhraseLower == null || r.Name.ToLower().Contains(searchPhraseLower) ||
                        r.Parent.ToLower().Contains(searchPhraseLower));


        var totalCount = await baseQuery.CountAsync(ct);

        baseQuery = baseQuery.OrderBy(x => x.Id);


        var permissions = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken: ct);

        return (permissions, totalCount);
    }


    public async Task<bool> ExistPermissionInPermissionSource(Guid permissionCode, Guid parentCode, CancellationToken ct)
    {
        return await dbContext.Permissions.AnyAsync(
     x => x.PermissionCode == permissionCode && x.ParentCode == parentCode,
     ct);
    }

    public async Task<int> AddPermissionToPermissionSource(Permission permission, CancellationToken ct)
    {
        dbContext.Permissions.Add(permission);
        await unitOfWork.SaveChangesAsync(ct);
        return permission.Id;
    }

    public void DeletePermission(Permission permission, CancellationToken cancellationToken)
    {
        dbContext.Entry(permission).Property("Deleted").CurrentValue = true;

    }

    //-------------
    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken)
    {
        var hasUserPermission = await dbContext.UserPermissions
            .AnyAsync(up => up.UserId == userId && up.Permission.Name == permission, cancellationToken: cancellationToken);

        var hasRolePermission = await dbContext.RolePermissions
            .Where(rp => dbContext.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == rp.RoleId))
            .AnyAsync(rp => rp.Permission.Name == permission, cancellationToken: cancellationToken);

        var hasGroupPermission = await dbContext.GroupPermissions
            .Where(gp => dbContext.UserGroups.Any(ug => ug.UserId == userId && ug.GroupId == gp.GroupId))
            .AnyAsync(gp => gp.Permission.Name == permission, cancellationToken: cancellationToken);

        return hasUserPermission || hasRolePermission || hasGroupPermission;
    }

    public async Task<bool> HasAnyPermissionAsync(string userId, CancellationToken cancellationToken)
    {
        var hasUserPermission = await dbContext.UserPermissions.AnyAsync(up => up.UserId == userId, cancellationToken: cancellationToken);
        Console.WriteLine($"UserPermissions for {userId}: {hasUserPermission}");

        var hasRolePermission = await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(dbContext.RolePermissions, ur => ur.RoleId, rp => rp.RoleId, (ur, rp) => rp)
            .AnyAsync(cancellationToken: cancellationToken);
        Console.WriteLine($"RolePermissions for {userId}: {hasRolePermission}");

        var hasGroupPermission = await dbContext.UserGroups
            .Where(ug => ug.UserId == userId)
            .Join(dbContext.GroupPermissions, ug => ug.GroupId, gp => gp.GroupId, (ug, gp) => gp)
            .AnyAsync(cancellationToken: cancellationToken);
        Console.WriteLine($"GroupPermissions for {userId}: {hasGroupPermission}");


        return hasUserPermission || hasRolePermission || hasGroupPermission;
    }

    public async Task<List<string>?> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        // 1.Getting UserPermission
        var userPermissionNames = await dbContext.UserPermissions
            .Where(up => up.UserId == userId)
            .Select(up => up.Permission.Name)
            .ToListAsync(cancellationToken: cancellationToken);

        // 2. GettingRoles (Role)
        var userRoles = await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken: cancellationToken);

        // 3. Getting RolePermission
        var rolePermissionNames = userRoles.Any()
            ? await dbContext.RolePermissions
                .Where(rp => userRoles.Contains(rp.RoleId))
                .Select(rp => rp.Permission.Name)
                .ToListAsync(cancellationToken: cancellationToken)
            : new List<string>();

        // 4. Getting  UserGroups
        var userGroupIds = await dbContext.UserGroups
            .Where(ug => ug.UserId == userId)
            .Select(ug => ug.GroupId)
            .ToListAsync(cancellationToken: cancellationToken);

        // 5. Getting GroupPermission
        var groupPermissionNames = userGroupIds.Any()
            ? await dbContext.GroupPermissions
                .Where(gp => userGroupIds.Contains(gp.GroupId))
                .Select(gp => gp.Permission.Name)
                .ToListAsync(cancellationToken: cancellationToken)
            : new List<string>();

        // 6. Combine all permissions and remove duplicates
        var allPermissions = userPermissionNames
            .Concat(rolePermissionNames)
            .Concat(groupPermissionNames)
            .Distinct()
            .ToList();

        return allPermissions;
    }

    public async Task<HashSet<PermissionInfo>> PermissionList(string userId, CancellationToken cancellationToken)
    {
        var resultHashSet = new HashSet<PermissionInfo>();

        // 1.  UserPermissions
        var userPermissions = await dbContext.UserPermissions
            .AsNoTracking()
            .Include(x => x.Permission)
            .Where(up => up.UserId == userId)
            .Select(x => new PermissionInfo
            {
                PermissionId = x.PermissionId,
                UserId = x.UserId,
                PermissionName = x.Permission.Name
            })
            .ToListAsync(cancellationToken);

        // اضافه کردن UserPermissions به HashSet
        resultHashSet.UnionWith(userPermissions);  // 

        // 2. دریافت RolePermissions
        var rolePermissions = await dbContext.RolePermissions
            .AsNoTracking()
            .Include(x => x.Permission)
            .Where(rp => dbContext.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == rp.RoleId))
            .Select(x => new PermissionInfo
            {
                PermissionId = x.PermissionId,
                UserId = userId,  // UserId از پارامتر ورودی گرفته می‌شود
                PermissionName = x.Permission.Name
            })
            .ToListAsync(cancellationToken);

        // اضافه کردن RolePermissions به HashSet
        resultHashSet.UnionWith(rolePermissions);  // یا از foreach استفاده کنید

        // 3. دریافت GroupPermissions
        var groupPermissions = await dbContext.GroupPermissions
            .AsNoTracking()
            .Include(x => x.Permission)
            .Where(gp => dbContext.UserGroups.Any(ug => ug.UserId == userId && ug.GroupId == gp.GroupId))
            .Select(x => new PermissionInfo
            {
                PermissionId = x.PermissionId,
                UserId = userId,  // UserId از پارامتر ورودی گرفته می‌شود
                PermissionName = x.Permission.Name
            })
            .ToListAsync(cancellationToken);

        // اضافه کردن GroupPermissions به HashSet
        resultHashSet.UnionWith(groupPermissions);  // یا از foreach استفاده کنید

        // بازگشت HashSet نهایی که حالا شامل تمام دسترسی‌ها است
        return resultHashSet;
    }


    public async Task<List<int>> AddPermissionToUser(List<UserPermission> permissions, CancellationToken cancellationToken)
    {
        dbContext.UserPermissions.AddRange(permissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return permissions.Select(x => x.PermissionId).ToList();

    }

    public async Task<List<int>> RemovePermissionFromUser(List<UserPermission> permissions, CancellationToken cancellationToken)
    {
        foreach (var permission in permissions)
        {
            dbContext.Entry(permission).Property("Deleted").CurrentValue = true;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return permissions.Select(x => x.PermissionId).ToList();

    }

    public async Task<bool> ExistPermission(int permissionId, string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserPermissions.AnyAsync(x => x.PermissionId == permissionId && x.UserId == userId,
            cancellationToken);
    }

    public async Task<bool> ExistPermission(UserPermission permission, CancellationToken cancellationToken)
    {
        return await dbContext.UserPermissions.AnyAsync(
            x => x.PermissionId == permission.PermissionId && x.UserId == permission.UserId,
            cancellationToken);
    }



    public async Task<List<int>> AddPermissionToGroup(List<GroupPermission> groupPermissions, CancellationToken cancellationToken)
    {
        dbContext.GroupPermissions.AddRange(groupPermissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return groupPermissions.Select(x => x.PermissionId).ToList();
    }

    public async Task<List<int>> RemovePermissionFromGroup(List<GroupPermission> groupPermissions, CancellationToken cancellationToken)
    {
        foreach (var permission in groupPermissions)
        {
            dbContext.Entry(permission).Property("Deleted").CurrentValue = true;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return groupPermissions.Select(x => x.PermissionId).ToList();
    }

    public async Task<bool> ExistPermissionToGroup(int permissionId, int groupId, CancellationToken cancellationToken)
    {
        return await dbContext.GroupPermissions.AnyAsync(x => x.PermissionId == permissionId && x.GroupId == groupId,
            cancellationToken);
    }

    public async Task<bool> ExistPermissionToGroup(GroupPermission groupPermission, CancellationToken cancellationToken)
    {
        return await dbContext.GroupPermissions.AnyAsync(
        x => x.PermissionId == groupPermission.PermissionId && x.GroupId == groupPermission.GroupId,
        cancellationToken);
    }

    public async Task<List<int>> AddPermissionToRole(List<RolePermission> rolePermissions, CancellationToken cancellationToken)
    {
        dbContext.RolePermissions.AddRange(rolePermissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return rolePermissions.Select(x => x.PermissionId).ToList();
    }

    public async Task<List<int>> RemovePermissionFromRole(List<RolePermission> rolePermissions, CancellationToken cancellationToken)
    {
        foreach (var permission in rolePermissions)
        {
            dbContext.Entry(permission).Property("Deleted").CurrentValue = true;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return rolePermissions.Select(x => x.PermissionId).ToList();
    }

    public async Task<bool> ExistPermissionToRole(int permissionId, string roleId, CancellationToken cancellationToken)
    {
        return await dbContext.RolePermissions.AnyAsync(x => x.PermissionId == permissionId && x.RoleId == roleId,
            cancellationToken);
    }

    public async Task<bool> ExistPermissionToRole(RolePermission rolePermission, CancellationToken cancellationToken)
    {
        return await dbContext.RolePermissions.AnyAsync(
            x => x.PermissionId == rolePermission.PermissionId && x.RoleId == rolePermission.RoleId,
            cancellationToken);
    }



}