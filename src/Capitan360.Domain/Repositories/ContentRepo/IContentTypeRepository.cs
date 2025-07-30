using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Repositories.ContentRepo;

public interface IContentTypeRepository
{

    Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken);
    void Delete(ContentType contentType);
    Task<ContentType?> GetContentTypeById(int id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypes(string? searchPhrase, int companyTypeId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<ContentType?> CheckExistContentTypeName(string contentTypeName, int companyTypeId,
        CancellationToken cancellationToken);


    Task<int> OrderContentType(int companyTypeId, CancellationToken cancellationToken);
    Task MoveContentTypeUpAsync(int companyTypeId, int contentTypeId, CancellationToken cancellationToken);
    Task MoveContentTypeDownAsync(int companyTypeId, int contentTypeId, CancellationToken cancellationToken);
    Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeId(int companyTypeId,
        CancellationToken cancellationToken);
}