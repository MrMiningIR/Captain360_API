using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Commands.Create;
using Capitan360.Application.Features.Addresses.Areas.Commands.Delete;
using Capitan360.Application.Features.Addresses.Areas.Commands.Update;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetById;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetCity;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetProvince;

namespace Capitan360.Application.Features.Addresses.Areas.Services;

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