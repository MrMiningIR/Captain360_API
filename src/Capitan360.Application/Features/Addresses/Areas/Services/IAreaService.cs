using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Commands.Create;
using Capitan360.Application.Features.Addresses.Areas.Commands.Delete;
using Capitan360.Application.Features.Addresses.Areas.Commands.Update;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetAllChildren;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetById;

namespace Capitan360.Application.Features.Addresses.Areas.Services;

public interface IAreaService
{
    Task<ApiResponse<int>> CreateAreaAsync(CreateAreaCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<AreaDto>>> GetAllAreas(GetAllChildrenAreaQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<AreaDto>> GetAreaByIdAsync(GetAreaByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteAreaAsync(DeleteAreaCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<AreaDto>> UpdateAreaAsync(UpdateAreaCommand command, CancellationToken cancellationToken);
}