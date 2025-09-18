using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Dtos;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanySmsPatterns")]
    [ApiController]
    [PermissionFilter("بخش پیامک", "N")]

    public class CompanySmsPatternsController(ICompanySmsPatternsService companySmsPatternsService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanySmsPatternsDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanySmsPatternsDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("دریافت لیست پیامک ها", "N1")]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanySmsPatternsDto>>>> GetAllCompanySmsPatterns([FromQuery] GetAllCompanySmsPatternsQuery getAllCompanySmsPatternsQuery, CancellationToken cancellationToken)
        {
            var response = await companySmsPatternsService.GetAllCompanySmsPatternsAsync(getAllCompanySmsPatternsQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanySmsPatternsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanySmsPatternsDto>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("دریافت پترن", "N2")]
        public async Task<ActionResult<ApiResponse<CompanySmsPatternsDto>>> GetCompanySmsPatternsById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companySmsPatternsService.GetCompanySmsPatternsByIdAsync(new GetCompanySmsPatternsByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("ایجاد پترن", "N3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanySmsPatterns(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
        {
            var response = await companySmsPatternsService.CreateCompanySmsPatternsAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("حذف پترن", "N4")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteCompanySmsPatterns([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companySmsPatternsService.DeleteCompanySmsPatternsAsync(new DeleteCompanySmsPatternsCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("آپدیت پترن", "N5")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanySmsPatterns([FromRoute] int id, UpdateCompanySmsPatternsCommand companySmsPatternsCommand, CancellationToken cancellationToken)
        {
            companySmsPatternsCommand.Id = id;
            var response = await companySmsPatternsService.UpdateCompanySmsPatternsAsync(companySmsPatternsCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}