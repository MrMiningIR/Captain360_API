using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Commands.AddNewAddressToCompany;
using Capitan360.Application.Services.AddressService.Commands.DeleteAddress;
using Capitan360.Application.Services.AddressService.Commands.MoveAddress;
using Capitan360.Application.Services.AddressService.Commands.UpdateAddress;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.AddressService.Queries.GetAddressById;
using Capitan360.Application.Services.AddressService.Queries.GetAllAddresses;
using Capitan360.Application.Services.AddressService.Services;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/Addresses")]
[ApiController]
[PermissionFilter("بخش آدرس")]
public class AddressesController(IAddressService addressService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AddressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AddressDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست آدرس ها")]
    public async Task<ActionResult<PagedResult<AddressDto>>> GetAllAddresses([FromQuery] GetAllAddressQuery getAllAddressQuery, CancellationToken cancellationToken)
    {
        var response = await addressService.GetAllAddressesByCompany(getAllAddressQuery, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("گرفتن آدرس")]
    public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await addressService.GetAddressByIdAsync(new GetAddressByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف آدرس")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteAddress([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await addressService.DeleteAddressAsync(new DeleteAddressCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت آدرس")]
    public async Task<ActionResult<ApiResponse<AddressDto>>> UpdateAddress([FromRoute] int id, UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken)
    {
        updateAddressCommand.Id = id;
        var response = await addressService.UpdateAddressAsync(updateAddressCommand, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("AddAddress")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("افزودن آدرس")]
    public async Task<ActionResult<ApiResponse<int>>> AddAddress([FromBody] AddNewAddressToCompanyCommand addNewAddressToCompanyCommand, CancellationToken cancellationToken)
    {

        var response = await addressService.AddNewAddressToCompanyAsync(addNewAddressToCompanyCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpAddress")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغیر چیدمان- بالا")]
    public async Task<ActionResult<ApiResponse<object>>> MoveUpAddress(MoveAddressUpCommand moveAddressUpCommand, CancellationToken cancellationToken)
    {
        var response = await addressService.MoveAddressUpAsync(moveAddressUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownAddress")]
    [PermissionFilter("تغیر چیدمان- پایین")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> MoveDownAddress(MoveAddressDownCommand moveAddressDownCommand, CancellationToken cancellationToken)
    {
        var response = await addressService.MoveAddressDownAsync(moveAddressDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}