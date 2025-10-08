using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveDown;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveUp;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;
using Capitan360.Application.Features.Addresses.Addresses.Services;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/Addresses")]
[ApiController]
[PermissionFilter("بخش آدرس", "A")]
public class AddressesController(IAddressService addressService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AddressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AddressDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست آدرس ها", "A1")]
    public async Task<ActionResult<PagedResult<AddressDto>>> GetAllAddresses([FromQuery] GetAllAddressQuery getAllAddressQuery, CancellationToken cancellationToken)
    {
        var response = await addressService.GetAllAddresssAsync(getAllAddressQuery, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("گرفتن آدرس", "A2")]
    public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await addressService.GetAddressByIdAsync(new GetAddressByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("حذف آدرس", "A3")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteAddress([FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await addressService.DeleteAddressAsync(new DeleteAddressCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CompanyDto>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("آپدیت آدرس", "A4")]
    public async Task<ActionResult<ApiResponse<AddressDto>>> UpdateAddress([FromRoute] int id, UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken)
    {
        updateAddressCommand.Id = id;
        var response = await addressService.UpdateAddressAsync(updateAddressCommand, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    //[HttpPost("AddAddress")]
    //[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    //[PermissionFilter("افزودن آدرس", "A5")]
    //public async Task<ActionResult<ApiResponse<int>>> AddAddress([FromBody] AddNewAddressToCompanyCommand addNewAddressToCompanyCommand, CancellationToken cancellationToken)
    //{
    //
    //    var response = await addressService.AddNewAddressToCompanyAsync(addNewAddressToCompanyCommand, cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}

    [HttpPost("MoveUpAddress")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]

    [PermissionFilter("تغیر چیدمان- بالا", "A6")]
    public async Task<ActionResult<ApiResponse<object>>> MoveUpAddress(MoveUpAddressCommand moveAddressUpCommand, CancellationToken cancellationToken)
    {
        var response = await addressService.MoveUpAddressAsync(moveAddressUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownAddress")]
    [PermissionFilter("تغیر چیدمان- پایین", "A7")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> MoveDownAddress(MoveDownAddressCommand moveAddressDownCommand, CancellationToken cancellationToken)
    {
        var response = await addressService.MoveDownAddressAsync(moveAddressDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}