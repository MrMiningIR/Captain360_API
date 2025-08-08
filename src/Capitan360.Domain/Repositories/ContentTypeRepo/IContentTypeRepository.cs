using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Repositories.ContentTypeRepo;

public interface IContentTypeRepository
{
    Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken);

    void Delete(ContentType contentType);

    Task<ContentType?> GetContentTypeByIdAsync(int contentTypeId, bool tracked, CancellationToken cancellationToken);

    Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypesAsync(string? searchPhrase, int companyTypeId, int active,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int? currentContentTypeId, int companyTypeId, CancellationToken cancellationToken);

    Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken);

    Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken);
}