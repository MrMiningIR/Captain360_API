using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IUserPermissionRepository
{
    Task<int> AssignPermissionToUser(UserPermission userPermission, CancellationToken cancellationToken);

    Task<List<int>> AssignPermissionsToUser(List<UserPermission> userPermissions, CancellationToken cancellationToken);

    Task<List<int>> AssignPermissionsToUser(List<string> permissionIds, string userId, CancellationToken cancellationToken);

    Task<List<int>> RemovePermissionsFromUser(List<string> permissionIds, string userId, CancellationToken cancellationToken);

    Task<List<string>> GetUserPermissionsByUserId(string userId, CancellationToken cancellationToken);

    Task<int> RemovePermissionFromUser(UserPermission userPermission, CancellationToken cancellationToken);

    Task<List<int>> RemovePermissionsFromUser(List<UserPermission> userPermissions, CancellationToken cancellationToken);

    Task<(IReadOnlyList<UserPermission>, int total)> GetAllUserPermissions(string userId, int pageSize, int pageNumber, CancellationToken cancellationToken);

    Task<UserPermission?> GetUserPermissionByPermissionIdAndUserId(string userId, int permissionId, CancellationToken cancellationToken);
}