using Capitan360.Application.Common;
using Capitan360.Application.Features.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Features.ContentTypeService.Commands.DeleteContentType;
using Capitan360.Application.Features.ContentTypeService.Commands.MoveDownContentType;
using Capitan360.Application.Features.ContentTypeService.Commands.MoveUpContentType;
using Capitan360.Application.Features.ContentTypeService.Commands.UpdateActiveState;
using Capitan360.Application.Features.ContentTypeService.Commands.Update;
using Capitan360.Application.Features.ContentTypeService.Queries.GetAll;
using Capitan360.Application.Features.ContentTypeService.Queries.GetById;
using Capitan360.Application.Features.ContentTypes.Dtos;

namespace Capitan360.Application.Features.ContentTypeService.Services;

public interface IContentTypeService
{

    Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand contentType, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(GetAllContentTypesQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(GetContentTypeByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveContentTypeUpCommand moveContentTypeUpCommand,
        CancellationToken cancellationToken);
    Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveContentTypeDownCommand moveContentTypeDownCommand,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetContentTypeActivityStatus(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken);
}

