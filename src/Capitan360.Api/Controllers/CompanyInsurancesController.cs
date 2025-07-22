using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.DeleteCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Queries.GetAllCompanyInsurances;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Services;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyInsurances")]
[ApiController]
[PermissionFilter("بخش بیمه")]
public class CompanyInsurancesController(ICompanyInsuranceService companyInsuranceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست بیمه")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyInsuranceDto>>>> GetAllCompanyInsurances(
        [FromQuery] GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetAllCompanyInsurances(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyInsuranceDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت بیمه")]
    public async Task<ActionResult<ApiResponse<CompanyInsuranceDto>>> GetCompanyInsuranceById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.GetCompanyInsuranceByIdAsync(new GetCompanyInsuranceByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ایجاد بیمه")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompanyInsurance(
        [FromBody] CreateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceService.CreateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف بیمه")]
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
    [PermissionFilter("آپدیت بیمه")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyInsurance(
        [FromRoute] int id, [FromBody] UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await companyInsuranceService.UpdateCompanyInsuranceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}