using Capitan360.Application.Common;
using Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.UserPermission.Dtos;
using Capitan360.Application.Features.UserPermission.Queries.GetUserPermissions;
using Capitan360.Application.Features.UserPermission.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/UserPermission")]
[ApiController]
public class UserPermissionController(IUserPermissionService permissionService) : ControllerBase
{
    [HttpGet("GetUserPermissionsByUserId")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<PagedResult<UserPermissionDto>>>> GetAllUserPermissions([FromQuery] GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        var response = await permissionService.GetMatchingAllUserPermissions(query, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("AssignPermissionToUser")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<int>>> AssignPermissionToUser([FromBody] AssignUserPermissionCommand command, CancellationToken cancellationToken)
    {
        var response = await permissionService.AssignPermissionToUser(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("RemovePermissionFromUser")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<int>>> RemovePermissionFromUser([FromBody] RemoveUserPermissionCommand command,
        CancellationToken cancellationToken)
    {
        var response = await permissionService.RemovePermissionFromUser(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("AssignPermissionsToUser")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<int>>> AssignPermissionsToUser([FromBody] AssignUserPermissionCommands commands, CancellationToken cancellationToken)
    {
        var response = await permissionService.AssignPermissionsToUser(commands, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("RemovePermissionsFromUser")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<UserPermissionDto>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<int>>> RemovePermissionsFromUser([FromBody] RemoveUserPermissionCommands commands,
        CancellationToken cancellationToken)
    {
        var response = await permissionService.RemovePermissionsFromUser(commands, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("UserPermissionOperation")]
    [ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<List<string>>>> UserPermissionOperation([FromBody] UpDeInlUserPermissionByIdsCommand commands, CancellationToken cancellationToken)
    {
        var response = await permissionService.UserPermissionOperation(commands, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}