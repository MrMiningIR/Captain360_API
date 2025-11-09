using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetById;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyDomesticPaths")]
[ApiController]
[PermissionFilter("بخش مسیر", "H")]
public class CompanyDomesticPathsController(ICompanyDomesticPathService companyDomesticPathsService) : ControllerBase
{
    [HttpGet("GetAllCompanyDomesticPaths")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("لیست مسیر", "H1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDomesticPathDto>>>> GetAllCompanyDomesticPaths(
        [FromQuery] GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.GetAllCompanyDomesticPathsAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyDomesticPathById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("دریافت مسیر", "H2")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathDto>>> GetCompanyDomesticPathById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.GetCompanyDomesticPathByIdAsync(new GetCompanyDomesticPathByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateCompanyDomesticPath")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("ایجاد مسیر", "H3")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyDomesticPath(
        [FromBody] CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.CreateCompanyDomesticPathAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteCompanyDomesticPath/{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("حذف مسیر", "H4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyDomesticPath(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.DeleteCompanyDomesticPathAsync(new DeleteCompanyDomesticPathCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyDomesticPath/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("آپدیت مسیر", "H5")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathDto>>> UpdateCompanyDomesticPath(
        [FromRoute] int id, [FromBody] UpdateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyDomesticPathsService.UpdateCompanyDomesticPathAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("ChangeCompanyDomesticPathActiveStatus")]
    [PermissionFilter("تغییر وضعیت مسیر", "H6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyDomesticPathActiveStatus([FromBody] UpdateActiveStateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.SetCompanyDomesticPathActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}