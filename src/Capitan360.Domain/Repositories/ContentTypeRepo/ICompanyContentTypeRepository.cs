using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyContentEntity;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Repositories.ContentTypeRepo;

public interface ICompanyContentTypeRepository
{
    Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypesAsync(string? searchPhrase, int companyId, int active, int pageSize,
    int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<int> InsertCompanyContentTypeForCompanyAsync(CompanyContentType companyContentType, CancellationToken ct);
    Task<int> UpdateCompanyContentTypeForCompanyAsync(CompanyContentType companyContentType, CancellationToken ct);
    Task<bool> CheckExistCompanyContentTypeNameAsync(string companyContentTypeName, int? currentCompanyContentTypeId, int companyId, CancellationToken cancellationToken);
    Task MoveCompanyContentTypeUpAsync(int companyContentTypeId, CancellationToken cancellationToken);
    Task MoveCompanyContentTypeDownAsync(int companyContentTypeId, CancellationToken cancellationToken);
    Task<int> GetCountCompanyContentTypeAsync(int companyId, CancellationToken cancellationToken);
    Task CreateCompanyContentTypesAsync(List<int> companiesId, ContentType contentType, CancellationToken cancellationToken);
    Task AddContentTypesToCompanyContentTypeAsync(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken);
    Task DeleteAllContentsByCompanyIdAsync(int companyId, CancellationToken cancellationToken);
    Task<bool> CheckExistAnyItemAsync(int companyId, CancellationToken cancellationToken);
    Task<CompanyContentType?> GetCompanyContentTypeByIdAsync(int companyContentTypeId, bool tracked, bool loadData,
        CancellationToken cancellationToken);

    Task DeleteCompanyContentTypeAsync(CompanyContentType companyContentType);

    Task<(IReadOnlyList<CompanyContentType>, int)> GetMatchingAllCompanyContentTypesAsync(string? searchPhrase,
        string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection,
        CancellationToken cancellationToken);
}