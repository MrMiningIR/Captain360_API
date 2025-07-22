using Capitan360.Application.Common;
using Capitan360.Application.Services.Permission.Dtos;
using Capitan360.Application.Services.Permission.Services;
using Capitan360.Domain.Dtos.TransferObject;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Capitan360.Api.Controllers
{
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController(IPermissionService permissionService) : ControllerBase
    {
        [HttpGet("GetAppAllPermissions")]
        [ProducesResponseType(typeof(ApiResponse<List<PermissionCollectorDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<PermissionCollectorDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<List<PermissionCollectorDto>>> GetAppAllPermissions()
        {
            var response = permissionService.GetSystemPermissions(Assembly.GetExecutingAssembly());

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAllPagedPermissions")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<IPermissionService.PermissionDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<PagedResult<IPermissionService.PermissionDto>>>> GetAllPagedPermissions([FromQuery]
            IPermissionService.GetAllMatchingPermissionsQuery query, CancellationToken cancellationToken)
        {
            var response = await permissionService.GetAllMatchingPermissions(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetPermissionParents")]
        [ProducesResponseType(typeof(ApiResponse<List<ParentPermissionTransfer>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<ParentPermissionTransfer>>>> GetPermissionParents(CancellationToken cancellationToken)
        {
            var response = await permissionService.GetParentPermissions(cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetPermissionByParentName")]
        [ProducesResponseType(typeof(ApiResponse<List<IPermissionService.PermissionDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<IPermissionService.PermissionDto>>>> GetPermissionByParentName([FromQuery] IPermissionService.GetPermissionsByParentNameQuery query,
            CancellationToken cancellationToken)
        {
            var response = await permissionService.GetPermissionsByParentName(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetPermissionByParentCode")]
        [ProducesResponseType(typeof(ApiResponse<List<IPermissionService.PermissionDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<IPermissionService.PermissionDto>>>> GetPermissionByParentCode([FromQuery] IPermissionService.GetPermissionsByParentCodeQuery query,
            CancellationToken cancellationToken)
        {
            var response = await permissionService.GetPermissionsByParentCode(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}