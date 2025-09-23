using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Services;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetAll;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Delete;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetById;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyInsurances")]
[ApiController]
[PermissionFilter("بخش بیمه", "K")]
public class CompanyInsurancesController(ICompanyInsuranceService companyInsuranceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست بیمه", "K1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyInsuranceDto>>>> GetAllCompanyInsurances(
        [FromQuery] GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetAllCompanyInsurancesAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("دریافت بیمه", "K2")]
    public async Task<ActionResult<ApiResponse<CompanyInsuranceDto>>> GetCompanyInsuranceById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetCompanyInsuranceByIdAsync(new GetCompanyInsuranceByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ایجاد بیمه", "K3")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyInsurance(
        [FromBody] CreateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.CreateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("حذف بیمه", "K4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCompanyInsurance(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.DeleteCompanyInsuranceAsync(new DeleteCompanyInsuranceCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت بیمه", "K5")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyInsurance(
        [FromRoute] int id, [FromBody] UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyInsuranceService.UpdateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}