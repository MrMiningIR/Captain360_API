using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.CreateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.DeleteCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetAllCompanyDomesticPaths;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetCompanyDomesticPathById;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyDomesticPaths")]
[ApiController]
[PermissionFilter("بخش مسیر")]
public class CompanyDomesticPathsController(ICompanyDomesticPathsService companyDomesticPathsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست مسیر")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDomesticPathDto>>>> GetAllCompanyDomesticPaths(
        [FromQuery] GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.GetAllCompanyDomesticPaths(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت مسیر")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathDto>>> GetCompanyDomesticPathById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.GetCompanyDomesticPathByIdAsync(new GetCompanyDomesticPathByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ایجاد مسیر")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyDomesticPath(
        [FromBody] CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.CreateCompanyDomesticPathAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف مسیر")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyDomesticPath(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyDomesticPathsService.DeleteCompanyDomesticPathAsync(new DeleteCompanyDomesticPathCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت مسیر")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathDto>>> UpdateCompanyDomesticPath(
        [FromRoute] int id, [FromBody] UpdateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyDomesticPathsService.UpdateCompanyDomesticPathAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}