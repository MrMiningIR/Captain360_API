using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Repositories.ContentTypeRepo;

public interface IContentTypeRepository
{
    Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken);

    Task Delete(ContentType contentType);

    Task<ContentType?> GetContentTypeByIdAsync(int contentTypeId, bool loadData, bool tracked,
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypesAsync(string? searchPhrase, string? sortBy,
        int companyTypeId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection,
        CancellationToken cancellationToken);

    Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int? currentContentTypeId, int companyTypeId, CancellationToken cancellationToken);

    Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken);

    Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked,
        bool loadData, CancellationToken cancellationToken);


}