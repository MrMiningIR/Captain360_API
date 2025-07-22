using Capitan360.Application.Services.AddressService.Commands.MoveAddress;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;

public record MoveContentTypeDownCommand(int CompanyTypeId, int ContentTypeId);