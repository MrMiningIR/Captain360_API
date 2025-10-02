using Capitan360.Application.Common;
using Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.UserPermission.Dtos;
using Capitan360.Application.Features.UserPermission.Queries.GetUserPermissions;

namespace Capitan360.Application.Features.Identities.UserPermission.Services;

public interface IUserPermissionService
{
    Task<ApiResponse<int>> AssignPermissionToUser(AssignUserPermissionCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<UserPermissionDto>>> GetMatchingAllUserPermissions(GetUserPermissionsQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> RemovePermissionFromUser(RemoveUserPermissionCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<List<int>>> AssignPermissionsToUser(AssignUserPermissionCommands commands, CancellationToken cancellationToken);

    Task<ApiResponse<List<int>>> RemovePermissionsFromUser(RemoveUserPermissionCommands commands, CancellationToken ccancellationTokent);

    Task<ApiResponse<List<string>>> UserPermissionOperation(UpDeInlUserPermissionByIdsCommand command, CancellationToken cancellationToken);
}