using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Commands.CreateArea;
using Capitan360.Application.Services.AddressService.Commands.DeleteArea;
using Capitan360.Application.Services.AddressService.Commands.UpdateArea;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.AddressService.Queries.GetAllAreas;
using Capitan360.Application.Services.AddressService.Queries.GetAreaById;
using Capitan360.Application.Services.AddressService.Queries.GetCityArea;
using Capitan360.Application.Services.AddressService.Queries.GetProvinceArea;

namespace Capitan360.Application.Services.AddressService.Services;

public interface IAreaService
{
    Task<ApiResponse<int>> CreateAreaAsync(CreateAreaCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<AreaDto>>> GetAllAreas(GetAllAreaQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<AreaDto>> GetAreaByIdAsync(GetAreaByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<object>> DeleteAreaAsync(DeleteAreaCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<AreaDto>> UpdateAreaAsync(UpdateAreaCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<AreaDto>>> GetAreasByParentIdAsync(int? parentId, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<ProvinceAreaDto>>> GetAllProvince(GetProvinceAreaQuery getAllAreaQuery, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CityAreaDto>>> GetAllCityByProvinceId(GetCityAreaQuery getAllAreaQuery,
        CancellationToken cancellationToken);

    Task<ApiResponse<List<AreaItemDto>>> GetDistricts(int cityId, CancellationToken cancellationToken);
}