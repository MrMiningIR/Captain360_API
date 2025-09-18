using Capitan360.Application.Common;
using Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.UserPermission.Dtos;
using Capitan360.Application.Features.UserPermission.Queries.GetUserPermissions;

namespace Capitan360.Application.Features.UserPermission.Services;

public interface IUserPermissionService
{
    Task<ApiResponse<int>> AssignPermissionToUser(AssignUserPermissionCommand command, CancellationToken ct);

    Task<ApiResponse<PagedResult<UserPermissionDto>>> GetMatchingAllUserPermissions(GetUserPermissionsQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> RemovePermissionFromUser(RemoveUserPermissionCommand command, CancellationToken ct);

    Task<ApiResponse<List<int>>> AssignPermissionsToUser(AssignUserPermissionCommands commands, CancellationToken ct);

    Task<ApiResponse<List<int>>> RemovePermissionsFromUser(RemoveUserPermissionCommands commands, CancellationToken ct);

    Task<ApiResponse<List<string>>> UserPermissionOperation(UpDeInlUserPermissionByIdsCommand command, CancellationToken ct);
}