
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Repositories.Identities;

public interface IGroupRepository
{
    Task<IReadOnlyList<Group>> GetGroupsAsync(CancellationToken cancellationToken);
   

}