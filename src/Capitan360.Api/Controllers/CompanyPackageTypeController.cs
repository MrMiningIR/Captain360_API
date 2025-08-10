using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeName;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetAllCompanyPackageTypes;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;
using Capitan360.Application.Services.CompanyPackageTypeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyPackageType")]
[ApiController]
[PermissionFilter("بخش بسته بندی شرکت", "L")]
public class CompanyPackageTypeController(ICompanyPackageTypeService companyPackageTypeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPackageTypeDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست بسته بندی شرکت", "L1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyPackageTypeDto>>>> GetAllCompanyPackageTypes(
        [FromQuery] GetAllCompanyPackageTypesQuery allCompanyPackageTypesQuery, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.GetAllCompanyPackageTypesByCompanyAsync(allCompanyPackageTypesQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyPackageTypeDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت بسته بندی شرکت", "L2")]
    public async Task<ActionResult<ApiResponse<CompanyPackageTypeDto>>> GetCompanyPackageTypeById(
    [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.GetCompanyPackageTypeByIdAsync(new GetCompanyPackageTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpCompanyPackageType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغییر چیدمان - بالا", "L4")]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpCompanyPackageType(
        [FromBody] MoveCompanyPackageTypeUpCommand movePackageTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.MoveCompanyPackageTypeUpAsync(movePackageTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownCompanyPackageType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغییر چیدمان - پایین", "L5")]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownCompanyPackageType(
        [FromBody] MoveCompanyPackageTypeDownCommand movePackageTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.MoveCompanyPackageTypeDownAsync(movePackageTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyPackageTypeNameAndDescription/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت نام", "L6")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPackageTypeNameAndDescription([FromRoute] int id,
    [FromBody] UpdateCompanyPackageTypeNameAndDescriptionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyPackageTypeService.UpdateCompanyPackageTypeNameAndDescriptionAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeCompanyPackageTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت بسته بندی مخصوص شرکت", "L7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyPackageTypeActiveStatus([FromBody] UpdateActiveStateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await companyPackageTypeService.SetCompanyPackageContentActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}