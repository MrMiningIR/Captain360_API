using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Repositories.PermissionRepository;

public interface IUserPermissionVersionControlRepository
{
    Task SetUserPermissionVersion(string userId, CancellationToken cancellationToken);
    Task<string> GetUserPermissionVersionString(string userId, CancellationToken cancellationToken);
    Task<UserPermissionVersionControl?> GetUserPermissionVersionObj(string userId, CancellationToken cancellationToken);

    Task UpdateUserPermissionVersion(string userId, string oldVersion, CancellationToken cancellationToken);
    void UpdateUserPermissionVersion(UserPermissionVersionControl pvc);

    Task DeleteUserPermissionVersion(string userId, string oldVersion, CancellationToken cancellationToken);
}