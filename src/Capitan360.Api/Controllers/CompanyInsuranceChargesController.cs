using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Services;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/CompanyInsuranceCharges")]
[ApiController]
[PermissionFilter("بخش نرخنامه بیمه", "J")]

public class CompanyInsuranceChargesController(ICompanyInsuranceChargeService companyInsuranceChargeService)
    : ControllerBase
{
    //[HttpGet]
    //[ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceChargeDto>>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyInsuranceChargeDto>>), StatusCodes.Status400BadRequest)]
    //[PermissionFilter("لیست بیمه", "J1")]
    //public async Task<ActionResult<ApiResponse<PagedResult<CompanyInsuranceChargeDto>>>> GetAllCompanyInsuranceCharges(
    //    [FromQuery] GetAllCompanyInsuranceChargesQuery query, CancellationToken cancellationToken)
    //{
    //    var response = await companyInsuranceChargeService.GetAllCompanyInsuranceCharges(query, cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}

    //[HttpGet("{id}")]
    //[ProducesResponseType(typeof(ApiResponse<CompanyInsuranceChargeDto>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse<CompanyInsuranceChargeDto>), StatusCodes.Status400BadRequest)]
    //
    //[PermissionFilter("دریافت نرخنامه", "J2")]
    //public async Task<ActionResult<ApiResponse<CompanyInsuranceChargeDto>>> GetCompanyInsuranceChargeById(
    //    [FromRoute] int id, CancellationToken cancellationToken)
    //{
    //    var response = await companyInsuranceChargeService.GetCompanyInsuranceChargeByIdAsync(new GetCompanyInsuranceChargeByIdQuery(id), cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("ثبت نرخنامه بیمه", "J3")]
    public async Task<ActionResult<ApiResponse<CompanyInsuranceChargeDto>>> UpdateCompanyInsuranceCharge(
        [FromBody] UpdateCompanyInsuranceChargeListCommand command, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceChargeService.UpdateCompanyInsuranceChargeAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyInsurancePathChargeTableData")]
    [ProducesResponseType(typeof(ApiResponse<List<CompanyInsuranceChargeService.CompanyInsuranceChargeTableDataDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<CompanyInsuranceChargeService.CompanyInsuranceChargeTableDataDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("نمایش نرخنامه بیمه", "J4")]
    public async Task<ActionResult<ApiResponse<List<CompanyInsuranceChargeService.CompanyInsuranceChargeTableDataDto>>>> GetCompanyInsurancePathChargeTableData(
[FromQuery] CompanyInsuranceChargeService.GetCompanyInsuranceChargeTableDataQuery query, CancellationToken cancellationToken)
    {
        var response = await companyInsuranceChargeService.GetTableDataAAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}