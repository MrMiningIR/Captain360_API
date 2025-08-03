using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.DeleteCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateActiveStateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetAllCompanies;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetCompanyById;
using Capitan360.Application.Services.CompanyServices.Company.Services;
using Capitan360.Application.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/Companies")]
[ApiController]
[PermissionFilter("بخش شرکت ها", "D")]
public class CompaniesController(ICompanyService companyService, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("دریافت لیست شرکت ها", "D1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDto>>>> GetAllCompanies(
        [FromQuery] GetAllCompanyQuery getAllCompanyQuery, CancellationToken cancellationToken)
    {
        var response = await companyService.GetAllCompanies(getAllCompanyQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت شرکت", "D2")]
    public async Task<ActionResult<ApiResponse<CompanyDto>>> GetCompanyById(
        [FromRoute] int id, [FromQuery] int userCompanyTypeId, CancellationToken cancellationToken)
    {
        var response = await companyService.GetCompanyByIdAsync(new GetCompanyByIdQuery(id, userCompanyTypeId), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ایجاد شرکت", "D3")]
    public async Task<ActionResult<ApiResponse<int>>> CreateCompany(
        [FromBody] CreateCompanyCommand companyCommand, CancellationToken cancellationToken)
    {
        var response = await companyService.CreateCompanyAsync(companyCommand, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف شرکت", "D4")]
    public async Task<ActionResult<ApiResponse<int>>> DeleteCompany(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyService.DeleteCompanyAsync(new DeleteCompanyCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت شرکت", "D5")]
    public async Task<ActionResult<ApiResponse<int>>> UpdateCompany([FromRoute] int id,

        [FromBody] UpdateCompanyCommand updateCompanyCommand,
        CancellationToken cancellationToken)
    {
        updateCompanyCommand.Id = id;

        var response = await companyService.UpdateCompanyAsync(updateCompanyCommand, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeCompanyActiveStatus")]
    [PermissionFilter("تغییر وضعیت شرکت", "D6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyActiveStatus([FromBody] UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await companyService.SetCompanyActivityStatus(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}