using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateDescription;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyContentType")]
[ApiController]
[PermissionFilter("بخش محتوی شرکت", "F")]
public class CompanyContentTypeController(ICompanyContentTypeService companyContentTypeService) : ControllerBase
{
    [HttpGet("GetAllCompanyContentTypes")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyContentTypeDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("لیست محتوی شرکت", "F1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyContentTypeDto>>>> GetAllCompanyContentTypes(
        [FromQuery] GetAllCompanyContentTypesQuery allCompanyContentTypesQuery, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.GetAllCompanyContentTypesAsync(allCompanyContentTypesQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyContentTypeById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status403Forbidden)]


    [PermissionFilter("گرفتن محتوی شرکت", "F2")]
    public async Task<ActionResult<ApiResponse<CompanyContentTypeDto>>> GetCompanyContentTypeById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.GetCompanyContentTypeByIdAsync(new GetCompanyContentTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("تغییر چیدمان -بالا", "F4")]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpCompanyContentType(
        [FromBody] MoveUpCompanyContentTypeCommand moveContentTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveUpCompanyContentTypeAsync(moveContentTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("تغییر چیدمان -پایین", "F5")]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownCompanyContentType(
        [FromBody] MoveDownCompanyContentTypeCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveDownCompanyContentTypeAsync(moveContentTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyContentTypeName/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("آپدیت نام محتوی", "F6")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyContentTypeName(
     [FromRoute] int id, [FromBody] UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyContentTypeService.UpdateCompanyContentTypeNameAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPut("UpdateCompanyContentTypeDescription/{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("آپدیت توضیحات محتوی", "F7")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyContentTypeDescription(
    [FromRoute] int id, [FromBody] UpdateCompanyContentTypeDescriptionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyContentTypeService.UpdateCompanyContentTypeDescriptionAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeCompanyContentTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت محتوای مخصوص شرکت", "F8")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyContentTypeActiveStatus([FromBody] UpdateActiveStateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.SetCompanyContentTypeActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}