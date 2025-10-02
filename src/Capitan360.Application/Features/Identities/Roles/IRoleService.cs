using Capitan360.Application.Common;
using Capitan360.Application.Features.Role.Commands;
using Capitan360.Application.Features.Role.Commands.CreateRole;
using Capitan360.Application.Features.Role.Commands.UpdateQuery;

namespace Capitan360.Application.Features.Identities.Roles;

public interface IRoleService
{
    Task<ApiResponse<string>> CreateRole(CreateRoleCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<string>> DeleteRole(DeleteRoleCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<string>> UpdateRole(UpdateRoleCommand command, CancellationToken cancellationToken);



}