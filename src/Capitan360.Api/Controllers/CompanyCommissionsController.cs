using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyCommissions")]
    [ApiController]
    [PermissionFilter("بخش کمیسیون", "E")]

    public class CompanyCommissionsController(ICompanyCommissionsService companyCommissionsService) : ControllerBase
    {
        [HttpGet("GetAllCompanyCommissions")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status401Unauthorized)]
        [PermissionFilter("دریافت لیست کمیسیسون", "E1")]

        public async Task<ActionResult<ApiResponse<PagedResult<CompanyCommissionsDto>>>> GetAllCompanyCommissions([FromQuery] GetAllCompanyCommissionsQuery getAllCompanyCommissionsQuery, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.GetAllCompanyCommissionsAsync(getAllCompanyCommissionsQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCompanyCommissionsById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status403Forbidden)]

        [PermissionFilter("دریافت کمیسیسون", "E2")]
        public async Task<ActionResult<ApiResponse<CompanyCommissionsDto>>> GetCompanyCommissionsById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.GetCompanyCommissionsByCompanyIdAsync(new GetCompanyCommissionsByCompanyIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateCompanyCommissions")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
        [PermissionFilter("ایجاد کمیسیون", "E3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyCommissions(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.CreateCompanyCommissionsAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DeleteCompanyCommissions/{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("حذف کمیسیون", "E4")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteCompanyCommissions([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.DeleteCompanyCommissionsAsync(new DeleteCompanyCommissionsCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCompanyCommissions/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status403Forbidden)]

        [PermissionFilter("آپدیت کمیسیون", "E5")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyCommissions([FromRoute] int id, UpdateCompanyCommissionsCommand updateCompanyCommissionsCommand, CancellationToken cancellationToken)
        {

            updateCompanyCommissionsCommand.Id = id;
            var response = await companyCommissionsService.UpdateCompanyCommissionsAsync(updateCompanyCommissionsCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}