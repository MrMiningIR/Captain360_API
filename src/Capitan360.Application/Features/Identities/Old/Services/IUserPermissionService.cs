using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Old.Commands.RemoveUserPermission;
using Capitan360.Application.Features.Identities.Old.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.Identities.Old.Queries.GetUserPermissions;
using Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.AssignPermissions;
using Capitan360.Application.Features.Identities.Users.UserPermissions.Dtos;

namespace Capitan360.Application.Features.Identities.Old.Services;

public interface IUserPermissionService
{
    Task<ApiResponse<int>> AssignPermissionToUser(AssignPermissionsToUserCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<UserPermissionDto>>> GetMatchingAllUserPermissions(GetUserPermissionsQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> RemovePermissionFromUser(RemoveUserPermissionCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<List<int>>> AssignPermissionsToUser(AssignPermissionsToUserCommand commands, CancellationToken cancellationToken);

    Task<ApiResponse<List<int>>> RemovePermissionsFromUser(RemoveUserPermissionCommands commands, CancellationToken ccancellationTokent);

    Task<ApiResponse<List<string>>> UserPermissionOperation(UpDeInlUserPermissionByIdsCommand command, CancellationToken cancellationToken);
}