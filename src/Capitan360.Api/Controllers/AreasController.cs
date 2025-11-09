using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Commands.Create;
using Capitan360.Application.Features.Addresses.Areas.Commands.Delete;
using Capitan360.Application.Features.Addresses.Areas.Commands.Update;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetAllChildren;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetById;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetCity;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetMunicipalArea;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetProvince;
using Capitan360.Application.Features.Addresses.Areas.Services;
using Capitan360.Application.Features.Identities.Identities.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController(IAreaService areaService, IUserContext userContext) : ControllerBase
    {
        [HttpGet("GetAllAreas")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<AreaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<AreaDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<AreaDto>>>> GetAllAreas(
            [FromQuery] GetAllChildrenAreaQuery getAllAreaQuery, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var response = await areaService.GetAllAreas(getAllAreaQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAllProvince")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<ProvinceAreaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<ProvinceAreaDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<ProvinceAreaDto>>>> GetAllProvince(
        [FromQuery] GetProvinceAreaQuery getProvinceAreaQuery, CancellationToken cancellationToken)
        {
            var response = await areaService.GetAllProvince(getProvinceAreaQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetMunicipalAreaByParentId")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<MunicipalAreaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<MunicipalAreaDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<MunicipalAreaDto>>>> GetMunicipalAreaByParentId(
           [FromQuery] GetMunicipalAreaQuery getMunicipalAreaQuery, CancellationToken cancellationToken)
        {
            var response = await areaService.GetMunicipalAreaByParentId(getMunicipalAreaQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetDistrictsByCityId")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<AreaItemDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<List<AreaItemDto>>>> GetDistrictsByCityId(
        [FromQuery] int id, CancellationToken cancellationToken)
        {
            var response = await areaService.GetDistricts(id, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCitiesByProvinceId")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CityAreaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CityAreaDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<CityAreaDto>>>> GetCitiesByProvinceId(
        [FromQuery] GetCityAreaQuery getCityAreaQuery, CancellationToken cancellationToken)
        {
            var response = await areaService.GetAllCityByProvinceId(getCityAreaQuery, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAreaById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<AreaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AreaDto>), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse<AreaDto>>> GetAreaById(
            [FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await areaService.GetAreaByIdAsync(new GetAreaByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateArea")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<int>>> CreateArea(
            [FromBody] CreateAreaCommand areaCommand, CancellationToken cancellationToken)
        {
            var response = await areaService.CreateAreaAsync(areaCommand, cancellationToken);
            if (!response.Success)
                return StatusCode(response.StatusCode, response);
            return CreatedAtAction(nameof(GetAreaById), new { id = response.Data }, response);
        }

        [HttpDelete("DeleteArea/{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse<object>>> DeleteArea(
            [FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await areaService.DeleteAreaAsync(new DeleteAreaCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPut("UpdateArea/{id}")]
        [ProducesResponseType(typeof(ApiResponse<AreaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AreaDto>), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse<AreaDto>>> UpdateArea(
            [FromBody] UpdateAreaCommand updateAreaCommand, CancellationToken cancellationToken)
        {
            var response = await areaService.UpdateAreaAsync(updateAreaCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAreasByParentId/{id}")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AreaDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<AreaDto>>>> GetAreasByParentId(
            [FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await areaService.GetAreasByParentIdAsync(id, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}