using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IGroupRepository
{
    Task<IReadOnlyList<Group>> GetGroupsAsync(CancellationToken cancellationToken);


}