using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetByDomesticPathId;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetById;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyDomesticPathReceiverCompanies")]
[ApiController]
[PermissionFilter("بخش نماینده مسیر", "C")]
public class CompanyDomesticPathReceiverCompaniesController(ICompanyDomesticPathReceiverCompanyService service) : ControllerBase
{
    [HttpPost("CreateCompanyDomesticPathReceiverCompany")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status409Conflict)]
    [PermissionFilter("ایجاد نماینده مسیر", "C1")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyDomesticPathReceiverCompany(
        [FromBody] CreateCompanyDomesticPathReceiverCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await service.CreateCompanyDomesticPathReceiverCompanyAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyDomesticPathReceiverCompany/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("ویرایش نماینده مسیر", "C2")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathReceiverCompanyDto>>> UpdateCompanyDomesticPathReceiverCompany(
        [FromRoute] int id, [FromBody] UpdateCompanyDomesticPathReceiverCompanyCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await service.UpdateCompanyDomesticPathReceiverCompanyAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteCompanyDomesticPathReceiverCompany/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [PermissionFilter("حذف نماینده مسیر", "C3")]
    public async Task<ActionResult> DeleteCompanyDomesticPathReceiverCompany(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        await service.DeleteCompanyDomesticPathReceiverCompanyAsync(new DeleteCompanyDomesticPathReceiverCompanyCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("GetAllCompanyDomesticPathReceiverCompanies")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status404NotFound)]
    [PermissionFilter("لیست نمایندگان مسیر", "C4")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>>> GetAllCompanyDomesticPathReceiverCompanies(
        [FromQuery] GetAllCompanyDomesticPathReceiverCompanyQuery query, CancellationToken cancellationToken)
    {
        var response = await service.GetAllCompanyDomesticPathReceiverCompaniesAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyDomesticPathReceiverCompanyById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDomesticPathReceiverCompanyDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت نماینده مسیر", "C5")]
    public async Task<ActionResult<ApiResponse<CompanyDomesticPathReceiverCompanyDto>>> GetCompanyDomesticPathReceiverCompanyById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await service.GetCompanyDomesticPathReceiverCompanyByIdAsync(new GetCompanyDomesticPathReceiverCompanyByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyDomesticPathReceiverCompanyByDomesticPathId/{domesticPathId}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت نمایندگان مسیر", "C6")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>>> GetCompanyDomesticPathReceiverCompanyByDomesticPathId(
        [FromRoute] int domesticPathId, CancellationToken cancellationToken)
    {
        var response = await service.GetCompanyDomesticPathReceiverCompanyByDomesticPathIdAsync(new GetCompanyDomesticPathReceiverCompanyByDomesticPathIdQuery(domesticPathId), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}

