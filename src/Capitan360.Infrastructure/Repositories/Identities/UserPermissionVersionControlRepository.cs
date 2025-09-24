using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class UserPermissionVersionControlRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IUserPermissionVersionControlRepository
{
    public async Task SetUserPermissionVersion(string userId, CancellationToken cancellationToken)
    {
        var newPermissionControl = Guid.NewGuid().ToString();
        dbContext.UserPermissionVersionControls.Add(new UserPermissionVersionControl()
        { PermissionVersion = newPermissionControl, UserId = userId });

        await unitOfWork.SaveChangesAsync(cancellationToken);

    }

    public async Task<string> GetUserPermissionVersionString(string userId, CancellationToken cancellationToken)
    {
        var permissionControl = await dbContext
            .UserPermissionVersionControls
            .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return permissionControl?.PermissionVersion ?? "";
    }

    public async Task<UserPermissionVersionControl?> GetUserPermissionVersionObj(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserPermissionVersionControls.SingleOrDefaultAsync(x => x.UserId == userId,
            cancellationToken);
    }

    public async Task UpdateUserPermissionVersion(string userId, string oldVersion, CancellationToken cancellationToken)
    {
        var oldVersionControl = await dbContext.UserPermissionVersionControls
            .SingleOrDefaultAsync(x => x.PermissionVersion == oldVersion && x.UserId == userId, cancellationToken);
        if (oldVersionControl is not null)
            oldVersionControl.PermissionVersion = Guid.NewGuid().ToString();

        await unitOfWork.SaveChangesAsync(cancellationToken);

    }

    public void UpdateUserPermissionVersion(UserPermissionVersionControl pvc)
    {
        pvc.PermissionVersion = Guid.NewGuid().ToString();
    }

    public async Task DeleteUserPermissionVersion(string userId, string oldVersion, CancellationToken cancellationToken)
    {
        var permissionControl = await dbContext
            .UserPermissionVersionControls
            .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        dbContext.Entry(permissionControl).Property("Deleted").CurrentValue = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}