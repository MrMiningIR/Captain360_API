using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Services;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;
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
        var response = await companyContentTypeService.GetAllCompanyContentTypesAsync(allCompanyContentTypesQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyContentTypeDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("گرفتن محتوی شرکت", "F2")]
    public async Task<ActionResult<ApiResponse<CompanyContentTypeDto>>> GetCompanyContentTypeById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.GetCompanyContentTypeByIdAsync(new GetCompanyContentTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]

    [PermissionFilter("تغییر چیدمان -بالا", "F4")]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpCompanyContentType(
        [FromBody] MoveUpCompanyContentTypeCommand moveContentTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveUpCompanyContentTypeAsync(moveContentTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownCompanyContentType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [PermissionFilter("تغییر چیدمان -پایین", "F5")]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownCompanyContentType(
        [FromBody] MoveDownCompanyContentTypeCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.MoveDownCompanyContentTypeAsync(moveContentTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    //[HttpPut("UpdateCompanyContentTypeNameAndDescription/{id}")]
    //[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    //
    //[PermissionFilter("آپدیت نام محتوی", "F6")]
    //public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyContentTypeNameAndDescription(
    //    [FromRoute] int id, [FromBody] UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken)
    //{
    //    command.Id = id;
    //    var response = await companyContentTypeService.UpdateCompanyContentTypeNameAsync(command, cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}

    [HttpPost("ChangeCompanyContentTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت محتوای مخصوص شرکت", "F7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyContentTypeActiveStatus([FromBody] UpdateActiveStateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await companyContentTypeService.SetCompanyContentTypeActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}