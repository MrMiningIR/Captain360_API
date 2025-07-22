using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyCommissions")]
    [ApiController]


    public class CompanyCommissionsController(ICompanyCommissionsService companyCommissionsService) : ControllerBase
    {
        [HttpGet]

        public async Task<IActionResult> GetAllCompanyCommissions([FromQuery] GetAllCompanyCommissionsQuery getAllCompanyCommissionsQuery, CancellationToken cancellationToken)
        {
            var companyCommissions = await companyCommissionsService.GetAllCompanyCommissions(getAllCompanyCommissionsQuery, cancellationToken);
            return Ok(companyCommissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyCommissionsById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var companyCommissions = await companyCommissionsService.GetCompanyCommissionsByIdAsync(new GetCompanyCommissionsByIdQuery(id), cancellationToken);
            return Ok(companyCommissions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyCommissions(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken)
        {
            var companyCommissionsId = await companyCommissionsService.CreateCompanyCommissionsAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetCompanyCommissionsById), new { id = companyCommissionsId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyCommissions([FromRoute] int id, CancellationToken cancellationToken)
        {
            await companyCommissionsService.DeleteCompanyCommissionsAsync(new DeleteCompanyCommissionsCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCompanyCommissions(UpdateCompanyCommissionsCommand command, CancellationToken cancellationToken)
        {
            await companyCommissionsService.UpdateCompanyCommissionsAsync(command, cancellationToken);
            return NoContent();
        }
    }
}