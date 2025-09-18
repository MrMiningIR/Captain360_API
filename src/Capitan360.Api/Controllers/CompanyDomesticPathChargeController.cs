using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Dtos;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionFilter("بخش نرخنامه", "G")]
    public class CompanyDomesticPathChargeController(ICompanyDomesticPathChargeService service) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status400BadRequest)]
        [ExcludeFromPermission]
        public async Task<ActionResult<ApiResponse<List<int>>>> CreateCompanyDomesticPathCharge(
            [FromBody] CreateCompanyDomesticPathChargeCommand command, CancellationToken cancellationToken)
        {
            var response = await service.CreateCompanyDomesticPathCharge(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("ثبت نرخنامه", "G2")]
        public async Task<ActionResult<ApiResponse<List<int>>>> UpdateCompanyDomesticPathCharge(
            [FromBody] UpdateCompanyDomesticPathChargeCommand command, CancellationToken cancellationToken)
        {
            var response = await service.UpdateCompanyDomesticPathCharge(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("لیست مسیرها", "G3")]
        [ExcludeFromPermission]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>>> GetAllCompanyDomesticPath(
            [FromQuery] GetAllCompanyDomesticPathChargeQuery query, CancellationToken cancellationToken)
        {
            var response = await service.GetAllCompanyDomesticPathCharge(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCompanyDomesticPathChargeTableData")]
        [ProducesResponseType(typeof(ApiResponse<List<CompanyDomesticPathChargeService.PathChargeTableDataDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<CompanyDomesticPathChargeService.PathChargeTableDataDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("نمایش نرخنامه-", "G4")]
        public async Task<ActionResult<ApiResponse<List<CompanyDomesticPathChargeService.PathChargeTableDataDto>>>> GetCompanyDomesticPathChargeTableData(
    [FromQuery] CompanyDomesticPathChargeService.GetCompanyDomesticPathChargeTableDataQuery query, CancellationToken cancellationToken)
        {
            var response = await service.GetTableDataAAsync(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}