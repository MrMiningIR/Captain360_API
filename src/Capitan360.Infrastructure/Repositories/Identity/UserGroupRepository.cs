using Capitan360.Domain.Repositories.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identity;

public class UserGroupRepository(ApplicationDbContext dbContext): IUserGroupRepository
{
    public async Task<IReadOnlyList<string>> GetUserGroupNameListAsyncByUserId(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserGroups
            .Where(ug => ug.UserId == userId)
            .Select(ug => ug.Group.Id.ToString())
            .ToListAsync(cancellationToken);
    }
}