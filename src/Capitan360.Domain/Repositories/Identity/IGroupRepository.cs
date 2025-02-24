using Capitan360.Domain.Entities.AuthorizationEntity;

namespace Capitan360.Domain.Repositories.Identity;

public interface IGroupRepository
{
    Task<IReadOnlyList<Group>> GetGroupsAsync(CancellationToken cancellationToken);
   

}