using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyContentTypeRepository
{
    Task CreateCompanyContentTypesAsync(List<int> companiesId, ContentType ContentType, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyContentTypeNameAsync(string companyContentTypeName, int? currentCompanyContentTypeId, int companyId, CancellationToken cancellationToken);

    Task<int> GetCountCompanyContentTypeAsync(int companyId, CancellationToken cancellationToken);

    Task<CompanyContentType?> GetCompanyContentTypeByIdAsync(int companyContentTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyContentType>?> GetCompanyContentTypeByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyContentTypeAsync(CompanyContentType cmpanyContentType);

    Task MoveCompanyContentTypeUpAsync(int companyContentTypeId, CancellationToken cancellationToken);

    Task MoveCompanyContentTypeDownAsync(int companyContentTypeId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyContentType>, int)> GetAllCompanyContentTypesAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task AddContentTypesToCompanyContentTypeAsync(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken);
}