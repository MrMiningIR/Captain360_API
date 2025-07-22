using Capitan360.Application.Common;
using Capitan360.Application.Services.ContentTypeService.Commands;
using Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.DeleteContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;
using Capitan360.Application.Services.ContentTypeService.Dtos;
using Capitan360.Application.Services.ContentTypeService.Queries.GetAllContentTypes;
using Capitan360.Application.Services.ContentTypeService.Queries.GetContentTypeById;

namespace Capitan360.Application.Services.ContentTypeService.Services;

public interface IContentTypeService
{

    Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand contentType, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(GetAllContentTypesQuery allContentTypesQuery, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(GetContentTypeByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteContentTypeAsync(DeleteContentTypeCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveContentTypeUpAsync(MoveContentTypeUpCommand moveContentTypeUpCommand, CancellationToken cancellationToken);
    Task<ApiResponse<object>> MoveContentTypeDownAsync(MoveContentTypeDownCommand moveContentTypeDownCommand, CancellationToken cancellationToken);

}

