using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.DeleteCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetAllCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesById;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyPreferences")]
    [ApiController]
    [PermissionFilter("بخش ترجیحات شرکت", "M")]
    public class CompanyPreferencesController(ICompanyPreferencesService companyPreferencesService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("دریافت ترجیحات شرکت ها", "M1")]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyPreferencesDto>>>> GetAllCompanyPreferences([FromQuery] GetAllCompanyPreferencesQuery getAllCompanyPreferencesQuery, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.GetAllCompanyPreferences(getAllCompanyPreferencesQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status404NotFound)]
        [PermissionFilter("دریافت ترجیح شرکت", "M2")]
        public async Task<ActionResult<ApiResponse<CompanyPreferencesDto>>> GetCompanyPreferencesById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.GetCompanyPreferencesByIdAsync(new GetCompanyPreferencesByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("ایجاد ترجیح", "M3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyPreferences(CreateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.CreateCompanyPreferencesAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        [PermissionFilter("حذف ترجیح", "M4")]
        public async Task<IActionResult> DeleteCompanyPreferences([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.DeleteCompanyPreferencesAsync(new DeleteCompanyPreferencesCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        [PermissionFilter("آپدیت ترجیح", "M5")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPreferences([FromRoute] int id, UpdateCompanyPreferencesCommand updateCompanyPreferencesCommand, CancellationToken cancellationToken)
        {
            updateCompanyPreferencesCommand.Id = id;
            var response = await companyPreferencesService.UpdateCompanyPreferencesAsync(updateCompanyPreferencesCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}