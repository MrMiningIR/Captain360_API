using Capitan360.Application.Common;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Application.Features.ContentTypes.Commands.Create;
using Capitan360.Application.Features.ContentTypes.Commands.Update;
using Capitan360.Application.Features.ContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.ContentTypes.Commands.Delete;
using Capitan360.Application.Features.ContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.ContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.ContentTypes.Queries.GetAll;
using Capitan360.Application.Features.ContentTypes.Queries.GetById;

namespace Capitan360.Application.Features.ContentTypeService.Services;

public interface IContentTypeService
{

    Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand contentType, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(GetAllContentTypesQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(GetContentTypeByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveUpContentTypeCommand moveContentTypeUpCommand,
        CancellationToken cancellationToken);
    Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveDownContentTypeCommand moveContentTypeDownCommand,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetContentTypeActivityStatus(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken);
}

