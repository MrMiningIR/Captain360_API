using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.ContentTypes;

public interface IContentTypeRepository
{
    Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int? currentContentTypeId, int companyTypeId, CancellationToken cancellationToken);

    Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken);

    Task<ContentType?> GetContentTypeByIdAsync(int contentTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeletePackageTypeAsync(ContentType contentType);

    Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<ContentType>, int)> GetAllContentTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);
}