using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class UserPermissionRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IUserPermissionRepository
{
    public async Task<int> AssignPermissionToUser(UserPermission userPermission, CancellationToken cancellationToken)
    {
        dbContext.UserPermissions.Add(userPermission);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return userPermission.Id;
    }

    public async Task<List<int>> AssignPermissionsToUser(List<UserPermission> userPermissions, CancellationToken cancellationToken)
    {
        //delete from userpermissions where userid = ??
        foreach (var permission in userPermissions)
        {
            var exist = await GetUserPermissionByPermissionIdAndUserId(permission.UserId, permission.PermissionId, cancellationToken);
            if (exist != null)
                continue;

            dbContext.UserPermissions.Add(permission);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return userPermissions.Select(x => x.Id).ToList();
    }

    public async Task<List<int>> AssignPermissionsToUser(List<string> permissionIds, string userId, CancellationToken cancellationToken)
    {
        List<int> convertedList = new List<int>();
        foreach (var permission in permissionIds)
        {
            var converted = int.TryParse(permission, out int convertedId);
            if (converted)
            {
                var exist = await GetUserPermissionByPermissionIdAndUserId(userId, convertedId, cancellationToken);

                if (exist is not null)
                    continue;

                dbContext.UserPermissions.Add(new UserPermission() { PermissionId = convertedId, UserId = userId });
                convertedList.Add(convertedId);
            }
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return convertedList;
    }

    public async Task<List<int>> RemovePermissionsFromUser(List<string> permissionIds, string userId, CancellationToken cancellationToken)
    {
        List<int> convertedList = new List<int>();
        foreach (var permission in permissionIds)
        {
            var converted = int.TryParse(permission, out int convertedId);
            if (converted)
            {
                var exist = await GetUserPermissionByPermissionIdAndUserId(userId, convertedId, cancellationToken);
                if (exist == null)
                    continue;
                dbContext.Entry(exist).Property("Deleted").CurrentValue = true;
            }
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return convertedList;
    }

    public async Task<List<string>> GetUserPermissionsByUserId(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserPermissions.AsNoTracking()
            .Include(x => x.Permission)
            .Where(x => x.UserId == userId && x.Permission.Active)
            .Select(x => x.Permission.Name).ToListAsync(cancellationToken);
    }


    public async Task<int> RemovePermissionFromUser(UserPermission userPermission, CancellationToken cancellationToken)
    {
        dbContext.Entry(userPermission).Property("Deleted").CurrentValue = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return userPermission.Id;
    }

    public async Task<List<int>> RemovePermissionsFromUser(List<UserPermission> userPermissions, CancellationToken cancellationToken)
    {
        foreach (var permission in userPermissions)
        {
            //var exist = await GetUserPermissionByPermissionIdAndUserId(permission.UserId, permission.PermissionId, ct);
            //if (exist == null)
            //    continue;
            dbContext.Entry(permission).Property("Deleted").CurrentValue = true;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return userPermissions.Select(x => x.PermissionId).ToList();
    }

    public async Task<(IReadOnlyList<UserPermission>, int)> GetAllUserPermissions(string userId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext
            .UserPermissions
            .AsNoTracking()
            .Include(x => x.Permission)
            .Where(x => x.UserId == userId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        baseQuery = baseQuery.OrderByDescending(x => x.Id);

        var userPermissions = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (userPermissions, totalCount);
    }

    public async Task<UserPermission?> GetUserPermissionByPermissionIdAndUserId(string userId, int permissionId, CancellationToken cancellationToken)
    {
        return await dbContext.UserPermissions.SingleOrDefaultAsync(
            x => x.PermissionId == permissionId && x.UserId == userId, cancellationToken);
    }
}