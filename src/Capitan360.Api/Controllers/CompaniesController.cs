using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.Companies.Commands.Create;
using Capitan360.Application.Features.Companies.Companies.Commands.Delete;
using Capitan360.Application.Features.Companies.Companies.Commands.Update;
using Capitan360.Application.Features.Companies.Companies.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Capitan360.Application.Features.Companies.Companies.Queries.GetAll;
using Capitan360.Application.Features.Companies.Companies.Queries.GetById;
using Capitan360.Application.Features.Companies.Companies.Services;
using Capitan360.Application.Features.Identities.Identities.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/Companies")]
[ApiController]
[PermissionFilter("بخش شرکت ها", "D")]
public class CompaniesController(ICompanyService companyService, IUserContext userContext) : ControllerBase
{
    [HttpGet("GetAllCompanies")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDto>>), StatusCodes.Status403Forbidden)]
    [PermissionFilter("دریافت لیست شرکت ها", "D1")]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDto>>>> GetAllCompanies(
        [FromQuery] GetAllCompanyQuery getAllCompanyQuery, CancellationToken cancellationToken)
    {
        var response = await companyService.GetAllCompanies(getAllCompanyQuery, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }


    [HttpGet("GetCompanyById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("دریافت شرکت", "D2")]
    public async Task<ActionResult<ApiResponse<CompanyDto>>> GetCompanyById(
        [FromRoute] int id, [FromQuery] int userCompanyTypeId, CancellationToken cancellationToken)
    {
        var response = await companyService.GetCompanyByIdAsync(new GetCompanyByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateCompany")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
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

    [PermissionFilter("حذف شرکت", "D4")]
    public async Task<ActionResult<ApiResponse<int>>> DeleteCompany(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await companyService.DeleteCompanyAsync(new DeleteCompanyCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPut("UpdateCompany/{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("آپدیت شرکت", "D5")]
    public async Task<ActionResult<ApiResponse<CompanyDto?>>> UpdateCompany([FromRoute] int id,

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

    public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyActiveStatus([FromBody] UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await companyService.SetCompanyActivityStatus(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}