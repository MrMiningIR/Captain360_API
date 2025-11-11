using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateInternationalAirlineCargoState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateIssueDomesticWaybillState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateShowInSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateWebServiceSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyPreferences")]
    [ApiController]
    [PermissionFilter("بخش ترجیحات شرکت", "M")]
    public class CompanyPreferencesController(ICompanyPreferencesService companyPreferencesService) : ControllerBase
    {
        [HttpGet("GetAllCompanyPreferences")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyPreferencesDto>>), StatusCodes.Status403Forbidden)]
        [PermissionFilter("دریافت ترجیحات شرکت ها", "M1")]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyPreferencesDto>>>> GetAllCompanyPreferences([FromQuery] GetAllCompanyPreferencesQuery getAllCompanyPreferencesQuery, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.GetAllCompanyPreferencesAsync(getAllCompanyPreferencesQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCompanyPreferencesById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status403Forbidden)]

        [PermissionFilter("دریافت ترجیح شرکت", "M2")]
        public async Task<ActionResult<ApiResponse<CompanyPreferencesDto>>> GetCompanyPreferencesById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.GetCompanyPreferencesByIdAsync(new GetCompanyPreferencesByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateCompanyPreferences")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
        [PermissionFilter("ایجاد ترجیح", "M3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyPreferences(CreateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.CreateCompanyPreferencesAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DeleteCompanyPreferencesAsync/{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("حذف ترجیح", "M4")]
        public async Task<IActionResult> DeleteCompanyPreferences([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.DeleteCompanyPreferencesAsync(new DeleteCompanyPreferencesCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCompanyPreferences/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status401Unauthorized)]

        [PermissionFilter("آپدیت ترجیح", "M5")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPreferences([FromRoute] int id, UpdateCompanyPreferencesCommand updateCompanyPreferencesCommand, CancellationToken cancellationToken)
        {
            updateCompanyPreferencesCommand.Id = id;
            var response = await companyPreferencesService.UpdateCompanyPreferencesAsync(updateCompanyPreferencesCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("SetCompanyIssueDomesticWaybill")]
        [PermissionFilter("تغییر صدور بارنامه داخلی", "M6")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<int>>> SetCompanyIssueDomesticWaybill([FromBody] UpdateIssueDomesticWaybillStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.SetCompanyIssueDomesticWaybillStatusAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("SetCompanyShowInSearchEngine")]
        [PermissionFilter("تغییر نمایش در موتور جستجو", "M7")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<int>>> SetCompanyShowInSearchEngine([FromBody] UpdateShowInSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.SetCompanyShowInSearchEngineStatusAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("SetCompanyWebServiceSearchEngine")]
        [PermissionFilter("تغییر نمایش در وب سرویس موتور جستجو", "M8")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<int>>> SetCompanyWebServiceSearchEngine([FromBody] UpdateWebServiceSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.SetCompanyWebServiceSearchEngineStatusAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("SetCompanyInternationalAirline")]
        [PermissionFilter("تغییر وضعیت بار بین المللی", "M9")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<int>>> SetCompanyInternationalAirline([FromBody] UpdateInternationalAirlineCargoStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.SetCompanyInternationalAirlineCargoStatusAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}