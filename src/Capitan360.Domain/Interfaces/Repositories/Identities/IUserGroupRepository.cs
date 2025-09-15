
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Repositories.Identities;

public interface IUserGroupRepository
{
    Task<IReadOnlyList<string>>GetUserGroupNameListAsyncByUserId(string userId, CancellationToken cancellationToken);

    Task AddUerToGroup(UserGroup userGroup, CancellationToken cancellationToken);
    void RemoveUserFromGroup(UserGroup userGroup, CancellationToken cancellationToken);

    Task<UserGroup?> GetUserGroupAsync(string userId, int groupId, CancellationToken cancellationToken);

}