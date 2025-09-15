using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class UserGroupRepository(ApplicationDbContext dbContext) : IUserGroupRepository
{
    public async Task<IReadOnlyList<string>> GetUserGroupNameListAsyncByUserId(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserGroups
            .Where(ug => ug.UserId == userId)
            .Select(ug => ug.Group.Id.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task AddUerToGroup(UserGroup userGroup, CancellationToken cancellationToken)
    {
        await dbContext.UserGroups.AddAsync(userGroup, cancellationToken);
    }

    public void  RemoveUserFromGroup(UserGroup userGroup, CancellationToken cancellationToken)
    {

        dbContext.Entry(userGroup).Property("Deleted").CurrentValue = true;
        dbContext.Entry(userGroup).Property("UpdateDate").CurrentValue = DateTime.UtcNow;

    }

    public async Task<UserGroup?> GetUserGroupAsync(string userId, int groupId, CancellationToken cancellationToken)
    {
        return await dbContext.UserGroups.SingleOrDefaultAsync(item => item.UserId == userId && item.GroupId == groupId,
            cancellationToken);
    }
}
    