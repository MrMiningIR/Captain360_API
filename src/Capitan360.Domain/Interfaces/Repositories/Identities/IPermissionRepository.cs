using Capitan360.Domain.Dtos;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IPermissionRepository
{
    Task<IReadOnlyList<Permission>> GetAllPolicy(CancellationToken cancellationToken);
    Task<List<int>> GetAllPermissionsId(CancellationToken cancellationToken);

    Task<List<ParentPermissionTransfer>> GetParentPermissions(CancellationToken cancellationToken);
    Task<List<Permission>> GetPermissionsByParentName(string parentName, CancellationToken cancellationToken);

    Task<List<Permission>> GetPermissionsByParentCode(Guid parentCode, CancellationToken cancellationToken);
    Task<List<Permission>> GetFullListPermission(CancellationToken cancellationToken);



    Task<(IReadOnlyList<Permission>, int)> GetAllMatchingPermissions(string searchPhrase,
        int pageSize, int pageNumber, CancellationToken cancellationToken);






    //---------------

    Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken);
    Task<bool> HasAnyPermissionAsync(string userId, CancellationToken cancellationToken);
    //Task<List<string>?> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken);

    Task<HashSet<PermissionInfo>> PermissionList(string userId, CancellationToken cancellationToken);

    //UserPermission
    Task<List<int>> AddPermissionToUser(List<UserPermission> permissions, CancellationToken cancellationToken);
    Task<List<int>> RemovePermissionFromUser(List<UserPermission> permissions, CancellationToken cancellationToken);
    Task<bool> ExistPermission(int permissionId, string userId, CancellationToken cancellationToken);
    Task<bool> ExistPermission(UserPermission permission, CancellationToken cancellationToken);


    // GroupPermission   
    Task<List<int>> AddPermissionToGroup(List<GroupPermission> groupPermissions, CancellationToken cancellationToken);
    Task<List<int>> RemovePermissionFromGroup(List<GroupPermission> groupPermissions,
        CancellationToken cancellationToken);
    Task<bool> ExistPermissionToGroup(int permissionId, int groupId, CancellationToken cancellationToken);
    Task<bool> ExistPermissionToGroup(GroupPermission groupPermission, CancellationToken cancellationToken);

    //RolePermission
    Task<List<int>> AddPermissionToRole(List<RolePermission> rolePermissions, CancellationToken cancellationToken);
    Task<List<int>> RemovePermissionFromRole(List<RolePermission> rolePermissions, CancellationToken cancellationToken);
    Task<bool> ExistPermissionToRole(int permissionId, string roleId, CancellationToken cancellationToken);
    Task<bool> ExistPermissionToRole(RolePermission rolePermission, CancellationToken cancellationToken);



    // Permissions
    Task<bool> ExistPermissionInPermissionSource(Guid permissionCode, Guid parentCode, CancellationToken cancellationToken);


    Task<int> AddPermissionToPermissionSource(Permission permission, CancellationToken cancellationToken);
    void DeletePermission(Permission permission, CancellationToken cancellationToken);


}