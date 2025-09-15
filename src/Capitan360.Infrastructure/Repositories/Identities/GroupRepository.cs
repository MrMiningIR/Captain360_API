using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class GroupRepository(ApplicationDbContext dbContext):IGroupRepository
{
    public async Task<IReadOnlyList<Group>> GetGroupsAsync(CancellationToken cancellationToken)
    {
      return await dbContext.Groups.ToListAsync(cancellationToken);
    }
}