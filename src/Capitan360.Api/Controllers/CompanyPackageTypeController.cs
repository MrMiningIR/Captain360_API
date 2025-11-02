using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateDescription;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyPackageType")]
[ApiController]
[PermissionFilter("بخش بسته بندی شرکت", "L")]
public class CompanyPackageTypeController(ICompanyPackageTypeService companyPackageTypeService) : ControllerBase
{
    [HttpGet("GetAllCompanyPackageTypes")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("لیست بسته بندی شرکت", "L1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyPackageTypeDto>>>> GetAllCompanyPackageTypes(
        [FromQuery] GetAllCompanyPackageTypesQuery allCompanyPackageTypesQuery, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.GetAllCompanyPackageTypesAsync(allCompanyPackageTypesQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyPackageTypeById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status403Forbidden)]


    [PermissionFilter("دریافت بسته بندی شرکت", "L2")]
    public async Task<ActionResult<ApiResponse<CompanyPackageTypeDto>>> GetCompanyPackageTypeById(
    [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.GetCompanyPackageTypeByIdAsync(new GetCompanyPackageTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpCompanyPackageType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("تغییر چیدمان - بالا", "L4")]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpCompanyPackageType(
        [FromBody] MoveUpCompanyPackageTypeCommand movePackageTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.MoveUpCompanyPackageTypeAsync(movePackageTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownCompanyPackageType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("تغییر چیدمان - پایین", "L5")]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownCompanyPackageType(
        [FromBody] MoveDownCompanyPackageTypeCommand movePackageTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.MoveDownCompanyPackageTypeAsync(movePackageTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyPackageTypeName/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("آپدیت نام", "L6")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPackageTypeName([FromRoute] int id,
    [FromBody] UpdateCompanyPackageTypeNameCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyPackageTypeService.UpdateCompanyPackageTypeNameAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeCompanyPackageTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت بسته بندی مخصوص شرکت", "L7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyPackageTypeActiveStatus([FromBody] UpdateActiveStateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.SetCompanyPackageTypeActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyPackageTypeDescription/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("آپدیت توضیحات", "L8")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPackageTypeDescription([FromRoute] int id,
[FromBody] UpdateCompanyPackageTypeDescriptionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyPackageTypeService.UpdateCompanyPackageTypeDescriptionAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

}