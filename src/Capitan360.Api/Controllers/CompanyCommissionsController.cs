using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyCommissions")]
    [ApiController]
    [PermissionFilter("بخش کمیسیون", "E")]

    public class CompanyCommissionsController(ICompanyCommissionsService companyCommissionsService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyCommissionsDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("دریافت لیست کمیسیسون", "E1")]

        public async Task<ActionResult<ApiResponse<PagedResult<CompanyCommissionsDto>>>> GetAllCompanyCommissions([FromQuery] GetAllCompanyCommissionsQuery getAllCompanyCommissionsQuery, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.GetAllCompanyCommissions(getAllCompanyCommissionsQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyCommissionsDto>), StatusCodes.Status404NotFound)]
        [PermissionFilter("دریافت کمیسیسون", "E2")]
        public async Task<ActionResult<ApiResponse<CompanyCommissionsDto>>> GetCompanyCommissionsById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.GetCompanyCommissionsByIdAsync(new GetCompanyCommissionsByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("ایجاد کمیسیون", "E3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyCommissions(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.CreateCompanyCommissionsAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        [PermissionFilter("حذف کمیسیون", "E4")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteCompanyCommissions([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyCommissionsService.DeleteCompanyCommissionsAsync(new DeleteCompanyCommissionsCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        [PermissionFilter("آپدیت کمیسیون", "E5")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyCommissions([FromRoute] int id, UpdateCompanyCommissionsCommand updateCompanyCommissionsCommand, CancellationToken cancellationToken)
        {

            updateCompanyCommissionsCommand.Id = id;
            var response = await companyCommissionsService.UpdateCompanyCommissionsAsync(updateCompanyCommissionsCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}