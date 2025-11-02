using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateCaptain360UriState;
using Capitan360.Application.Features.Companies.CompanyUris.Dtos;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyUris.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;


[Route("api/CompanyUris")]
[ApiController]
[PermissionFilter(displayName: "بخش لینک شرکت", "P")]
public class CompanyUrisController(ICompanyUriService companyUriService) : ControllerBase
{
    [HttpGet("GetAllCompanyUris")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter(displayName: "لیست لینک شرکت", "P1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyUriDto>>>> GetAllCompanyUris(
        [FromQuery] GetAllCompanyUrisQuery query, CancellationToken cancellationToken)
    {
        var response = await companyUriService.GetAllCompanyUrisAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyUriById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status403Forbidden)]

    [PermissionFilter(displayName: "دریافت لینک شرکت", "P2")]
    public async Task<ActionResult<ApiResponse<CompanyUriDto>>> GetCompanyUriById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyUriService.GetCompanyUriByIdAsync(new GetCompanyUriByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateCompanyUri")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [PermissionFilter(displayName: "افزودن لینک شرکت", "P3")]

    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyUri(
        [FromBody] CreateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        var response = await companyUriService.CreateCompanyUriAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteCompanyUri/{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]

    [PermissionFilter(displayName: "حذف لینک شرکت", "P4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyUri(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyUriService.DeleteCompanyUriAsync(new DeleteCompanyUriCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyUri/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status403Forbidden)]

    [PermissionFilter(displayName: "آپدیت لینک شرکت", "P5")]

    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyUri(
        [FromRoute] int id, [FromBody] UpdateCompanyUriCommand updateCompanyUriCommand, CancellationToken cancellationToken)
    {
        updateCompanyUriCommand.Id = id;
        var response = await companyUriService.UpdateCompanyUriAsync(updateCompanyUriCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }


    [HttpPost("ChangeCompanyUriActiveStatus")]
    [PermissionFilter("تغییر وضعیت uri شرکت", "P6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyUriActiveStatus([FromBody] UpdateActiveStateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        var response = await companyUriService.SetCompanyUriActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("ChangeCaptain360UriActiveStatus")]
    [PermissionFilter("تغییر وضعیت uri360 شرکت", "P7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCaptain360UriActiveStatus([FromBody] UpdateCaptain360UriStateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        var response = await companyUriService.SetCompanyUriCaptain360UriStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}