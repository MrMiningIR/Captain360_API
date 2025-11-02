using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.PackageTypes.Commands.Create;
using Capitan360.Application.Features.PackageTypes.Commands.Delete;
using Capitan360.Application.Features.PackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.PackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypes.Commands.Update;
using Capitan360.Application.Features.PackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.PackageTypes.Queries.GetAll;
using Capitan360.Application.Features.PackageTypes.Queries.GetById;
using Capitan360.Application.Features.PackageTypes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/PackageTypes")]
[ApiController]
[PermissionFilter(displayName: "بخش بسته بندی", "U")]
public class PackageTypesController(IPackageTypeService packageTypeService) : ControllerBase
{
    [HttpGet("GetAllPackageTypes")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<PackageTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<PackageTypeDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<PackageTypeDto>>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<PackageTypeDto>>), StatusCodes.Status401Unauthorized)]
    [PermissionFilter(displayName: "لیست بسته بندی", "U1")]
    public async Task<ActionResult<ApiResponse<PagedResult<PackageTypeDto>>>> GetAllPackageTypes(
        [FromQuery] GetAllPackageTypesQuery query, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.GetAllPackageTypesAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetPackageTypeById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status403Forbidden)]
    [PermissionFilter(displayName: "دریافت بسته بندی", "U2")]
    public async Task<ActionResult<ApiResponse<PackageTypeDto>>> GetPackageTypeById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.GetPackageTypeByIdAsync(new GetPackageTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreatePackageType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter(displayName: "افزودن بسته بندی", "U3")]
    public async Task<ActionResult<ApiResponse<int>>> CreatePackageType(
        [FromBody] CreatePackageTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.CreatePackageTypeAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter(displayName: "حذف بسته بندی", "U4")]
    public async Task<ActionResult<ApiResponse<int>>> DeletePackageType(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.DeletePackageTypeAsync(new DeletePackageTypeCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdatePackageType/{id}")]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<PackageTypeDto>), StatusCodes.Status401Unauthorized)]
    [PermissionFilter(displayName: "آپدیت بسته بندی", "U5")]

    public async Task<ActionResult<ApiResponse<PackageTypeDto>>> UpdatePackageType([FromRoute] int id,
        [FromBody] UpdatePackageTypeCommand updatePackageTypeCommand, CancellationToken cancellationToken)
    {
        updatePackageTypeCommand.Id = id;

        var response = await packageTypeService.UpdatePackageTypeAsync(updatePackageTypeCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpPackageType")]
    [PermissionFilter("تغییر ترتیب - بالا", "U6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpPackagePackageType(MoveUpPackageTypeCommand movePackageTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.MoveUpPackageTypeAsync(movePackageTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownPackageType")]
    [PermissionFilter("تغییر ترتیب - بالا", "U7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownPackagePackageType(MoveDownPackageTypeCommand movePackageTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.MoveDownPackageTypeAsync(movePackageTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("ChangePackageTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت بسته بندی", "U8")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangePackageTypeActiveStatus([FromBody] UpdateActiveStatePackageTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await packageTypeService.SetPackageTypeActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}