using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Delete;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetAll;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetById;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyInsurances")]
[ApiController]
[PermissionFilter("بخش بیمه", "K")]
public class CompanyInsurancesController(ICompanyInsuranceService companyInsuranceService) : ControllerBase
{
    [HttpGet("GetAllCompanyInsurances")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("لیست بیمه", "K1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyInsuranceDto>>>> GetAllCompanyInsurances(
        [FromQuery] GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetAllCompanyInsurancesAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyInsuranceById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("دریافت بیمه", "K2")]
    public async Task<ActionResult<ApiResponse<CompanyInsuranceDto>>> GetCompanyInsuranceById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetCompanyInsuranceByIdAsync(new GetCompanyInsuranceByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateCompanyInsurance")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("ایجاد بیمه", "K3")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyInsurance(
        [FromBody] CreateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.CreateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("DeleteCompanyInsurance/{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]

    [PermissionFilter("حذف بیمه", "K4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyInsurance(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.DeleteCompanyInsuranceAsync(new DeleteCompanyInsuranceCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateCompanyInsurance/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("آپدیت بیمه", "K5")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyInsurance(
        [FromRoute] int id, [FromBody] UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyInsuranceService.UpdateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost("ChangeCompanyInsuranceActiveStatus")]
    [PermissionFilter("تغییر وضعیت شرکت بیمه", "K6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyInsuranceActiveStatus([FromBody] UpdateActiveStateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.SetCompanyInsuranceActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}