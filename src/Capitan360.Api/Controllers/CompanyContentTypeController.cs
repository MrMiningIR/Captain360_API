using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetCompanyContentTypeById;
using Capitan360.Application.Services.CompanyContentTypeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyContentType")]
[ApiController]
[PermissionFilter("بخش محتوی شرکت", "F")]
public class CompanyContentTypeController(ICompanyContentTypeService companyContentTypeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست محتوی شرکت", "F1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyContentTypeDto>>>> GetAllCompanyContentTypes(
        [FromQuery] GetAllCompanyContentTypesQuery allCompanyContentTypesQuery, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.GetAllCompanyContentTypesByCompany(allCompanyContentTypesQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("گرفتن محتوی شرکت", "F2")]
    public async Task<ActionResult<ApiResponse<CompanyContentTypeDto>>> GetCompanyContentTypeById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.GetCompanyContentTypeByIdAsync(new GetCompanyContentTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت محتوی شرکت", "F3")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyContentType(
        [FromRoute] int id, [FromBody] UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyContentTypeService.UpdateCompanyContentTypeAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغییر چیدمان -بالا", "F4")]

    public async Task<ActionResult<ApiResponse<object>>> MoveUpCompanyContentType(
        [FromBody] MoveCompanyContentTypeUpCommand moveContentTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveContentTypeUpAsync(moveContentTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغییر چیدمان -پایین", "F5")]
    public async Task<ActionResult<ApiResponse<object>>> MoveDownCompanyContentType(
        [FromBody] MoveCompanyContentTypeDownCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveContentTypeDownAsync(moveContentTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyContentTypeName/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت نام محتوی", "F6")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyContentTypeName(
        [FromRoute] int id, [FromBody] UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyContentTypeService.UpdateCompanyContentTypeNameAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeCompanyContentTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت محتوای مخصوص شرکت", "F7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyContentTypeActiveStatus([FromBody] UpdateActiveStateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.SetCompanyContentActivityStatus(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}