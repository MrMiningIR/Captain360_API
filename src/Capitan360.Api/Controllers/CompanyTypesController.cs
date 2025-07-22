using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.CreateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.DeleteCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetAllCompanyTypes;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetCompanyTypeById;
using Capitan360.Application.Services.CompanyServices.CompanyType.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyTypes")]
    [ApiController]
    public class CompanyTypesController(ICompanyTypeService companyTypeService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyTypeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyTypeDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyTypeDto>>>> GetAllCompanyTypes([FromQuery] GetAllCompanyTypesQuery getAllCompanyTypesQuery, CancellationToken cancellationToken)
        {
            var response = await companyTypeService.GetAllCompanyTypes(getAllCompanyTypesQuery, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyTypeDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyTypeDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> GetCompanyTypeById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyTypeService.GetCompanyTypeByIdAsync(new GetCompanyTypeByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyType(CreateCompanyTypeCommand command, CancellationToken cancellationToken)
        {
            var companyTypeId = await companyTypeService.CreateCompanyTypeAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetCompanyTypeById), new { id = companyTypeId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyType([FromRoute] int id, CancellationToken cancellationToken)
        {
            await companyTypeService.DeleteCompanyTypeAsync(new DeleteCompanyTypeCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCompanyType(UpdateCompanyTypeCommand command, CancellationToken cancellationToken)
        {
            await companyTypeService.UpdateCompanyTypeAsync(command, cancellationToken);
            return NoContent();
        }
    }
}