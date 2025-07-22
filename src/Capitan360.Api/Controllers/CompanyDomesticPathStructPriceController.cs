using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Update;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[PermissionFilter("بخش هزینه ها")]
public class CompanyDomesticPathStructPriceController(ICompanyDomesticPathStructPricesService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status400BadRequest)]
    [ExcludeFromPermission]
    public async Task<ActionResult<ApiResponse<List<int>>>> CreateCompanyDomesticPathStructPrice(
        [FromBody] CreateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken)
    {
        var response = await service.CreateStructPathByList(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<List<int>>), StatusCodes.Status404NotFound)]
    [PermissionFilter("ثبت هزینه ها - مبدا،مقصد")]
    public async Task<ActionResult<ApiResponse<List<int>>>> UpdateCompanyDomesticPathStructPrice(
        [FromBody] UpdateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken)
    {
        var response = await service.UpdateCompanyDomesticPathStructPriceAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>), StatusCodes.Status400BadRequest)]
    [ExcludeFromPermission]
    public async Task<ActionResult<ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>>> GetAllCompanyDomesticPath(
        [FromQuery] GetAllCompanyDomesticPathStructQuery query, CancellationToken cancellationToken)
    {
        var response = await service.GetAllCompanyDomesticPathStructPrices(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCompanyDomesticPathStructPriceTableData")]
    [ProducesResponseType(typeof(ApiResponse<List<TableDataDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<TableDataDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("نمایش هزینه ها")]

    public async Task<ActionResult<ApiResponse<List<TableDataDto>>>> GetCompanyDomesticPathStructPriceTableData(
        [FromQuery] GetCompanyDomesticPathStructPriceTableDataQuery query, CancellationToken cancellationToken)
    {
        var response = await service.GetTableDataAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}