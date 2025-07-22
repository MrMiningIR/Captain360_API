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
    public class CompanyPreferencesController(ICompanyPreferencesService companyPreferencesService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCompanyPreferences([FromQuery] GetAllCompanyPreferencesQuery getAllCompanyPreferencesQuery, CancellationToken cancellationToken)
        {
            var companyPreferences = await companyPreferencesService.GetAllCompanyPreferences(getAllCompanyPreferencesQuery, cancellationToken);
            return Ok(companyPreferences);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyPreferencesDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<CompanyPreferencesDto>>> GetCompanyPreferencesById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.GetCompanyPreferencesByIdAsync(new GetCompanyPreferencesByIdQuery(id), cancellationToken);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyPreferences(CreateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.CreateCompanyPreferencesAsync(command, cancellationToken);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyPreferences([FromRoute] int id, CancellationToken cancellationToken)
        {
            await companyPreferencesService.DeleteCompanyPreferencesAsync(new DeleteCompanyPreferencesCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyPreferences(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken)
        {
            var response = await companyPreferencesService.UpdateCompanyPreferencesAsync(command, cancellationToken);
            return StatusCode(Response.StatusCode, response);
        }
    }
}