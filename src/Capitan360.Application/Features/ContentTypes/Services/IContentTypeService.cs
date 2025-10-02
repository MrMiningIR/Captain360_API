using Capitan360.Application.Common;
using Capitan360.Application.Features.ContentTypes.Commands.Create;
using Capitan360.Application.Features.ContentTypes.Commands.Delete;
using Capitan360.Application.Features.ContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.ContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.ContentTypes.Commands.Update;
using Capitan360.Application.Features.ContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Application.Features.ContentTypes.Queries.GetAll;
using Capitan360.Application.Features.ContentTypes.Queries.GetById;

namespace Capitan360.Application.Features.ContentTypes.Services;

public interface IContentTypeService
{
    Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveUpContentTypeAsync(MoveUpContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownContentTypeAsync(MoveDownContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetContentTypeActivityStatusAsync(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypesAsync(GetAllContentTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(GetContentTypeByIdQuery query, CancellationToken cancellationToken);
}