using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;


[Route("api/CompanyUris")]
[ApiController]
[PermissionFilter(displayName: "بخش لینک شرکت", "P")]
public class CompanyUrisController(ICompanyUriService companyUriService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyUriDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter(displayName: "لیست لینک شرکت", "P1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyUriDto>>>> GetAllCompanyUris(
        [FromQuery] GetAllCompanyUrisQuery query, CancellationToken cancellationToken)
    {
        var response = await companyUriService.GetAllCompanyUrisAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyUriDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter(displayName: "دریافت لینک شرکت", "P2")]
    public async Task<ActionResult<ApiResponse<CompanyUriDto>>> GetCompanyUriById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyUriService.GetCompanyUriByIdAsync(new GetCompanyUriByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter(displayName: "افزودن لینک شرکت", "P3")]

    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyUri(
        [FromBody] CreateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        var response = await companyUriService.CreateCompanyUriAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter(displayName: "حذف لینک شرکت", "P4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyUri(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyUriService.DeleteCompanyUriAsync(new DeleteCompanyUriCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter(displayName: "آپدیت لینک شرکت", "P5")]

    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyUri(
        [FromRoute] int id, [FromBody] UpdateCompanyUriCommand updateCompanyUriCommand, CancellationToken cancellationToken)
    {
        updateCompanyUriCommand.Id = id;
        var response = await companyUriService.UpdateCompanyUriAsync(updateCompanyUriCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}