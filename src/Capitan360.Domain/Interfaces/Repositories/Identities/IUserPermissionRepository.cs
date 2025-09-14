using Capitan360.Domain.Entities.Authorizations;

namespace Capitan360.Domain.Repositories.Identities;

public interface IUserPermissionRepository
{
    Task<int> AssignPermissionToUser(UserPermission userPermission, CancellationToken ct);

    Task<List<int>> AssignPermissionsToUser(List<UserPermission> userPermissions, CancellationToken ct);

    Task<List<int>> AssignPermissionsToUser(List<string> permissionIds, string userId, CancellationToken ct);

    Task<List<int>> RemovePermissionsFromUser(List<string> permissionIds, string userId, CancellationToken ct);

    Task<List<string>> GetUserPermissionsByUserId(string userId, CancellationToken ct);

    Task<int> RemovePermissionFromUser(UserPermission userPermission, CancellationToken ct);

    Task<List<int>> RemovePermissionsFromUser(List<UserPermission> userPermissions, CancellationToken ct);

    Task<(IReadOnlyList<UserPermission>, int total)> GetAllUserPermissions(string userId, int pageSize, int pageNumber, CancellationToken cancellationToken);

    Task<UserPermission?> GetUserPermissionByPermissionIdAndUserId(string userId, int permissionId, CancellationToken cancellationToken);
}