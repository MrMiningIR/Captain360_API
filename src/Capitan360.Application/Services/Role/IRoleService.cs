using Capitan360.Application.Common;
using Capitan360.Application.Services.Role.Commands;
using Capitan360.Application.Services.Role.Commands.CreateRole;
using Capitan360.Application.Services.Role.Commands.UpdateQuery;

namespace Capitan360.Application.Services.Role;

public interface IRoleService
{
    Task<ApiResponse<string>> CreateRole(CreateRoleCommand command);

    Task<ApiResponse<string>> DeleteRole(DeleteRoleCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<string>> UpdateRole(UpdateRoleCommand command);



}