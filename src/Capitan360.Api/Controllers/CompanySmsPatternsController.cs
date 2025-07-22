using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanySmsPatterns")]
    [ApiController]
    public class CompanySmsPatternsController(ICompanySmsPatternsService companySmsPatternsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCompanySmsPatterns([FromQuery] GetAllCompanySmsPatternsQuery getAllCompanySmsPatternsQuery, CancellationToken cancellationToken)
        {
            var companySmsPatterns = await companySmsPatternsService.GetAllCompanySmsPatterns(getAllCompanySmsPatternsQuery, cancellationToken);
            return Ok(companySmsPatterns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanySmsPatternsById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var companySmsPatterns = await companySmsPatternsService.GetCompanySmsPatternsByIdAsync(new GetCompanySmsPatternsByIdQuery(id), cancellationToken);
            return Ok(companySmsPatterns);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanySmsPatterns(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
        {
            var companySmsPatternsId = await companySmsPatternsService.CreateCompanySmsPatternsAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetCompanySmsPatternsById), new { id = companySmsPatternsId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanySmsPatterns([FromRoute] int id, CancellationToken cancellationToken)
        {
            await companySmsPatternsService.DeleteCompanySmsPatternsAsync(new DeleteCompanySmsPatternsCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCompanySmsPatterns(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
        {
            await companySmsPatternsService.UpdateCompanySmsPatternsAsync(command, cancellationToken);
            return NoContent();
        }
    }
}