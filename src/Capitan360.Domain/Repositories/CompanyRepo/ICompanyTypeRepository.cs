using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyTypeRepository
{

    Task<bool> CheckExistCompanyTypeNameAsync(string companyTypeName, int? currentCompanyTypeId, CancellationToken cancellationToken);

    Task<int> CreateCompanyTypeAsync(CompanyType companyType, CancellationToken cancellationToken);

    Task<CompanyType?> GetCompanyTypeByIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyTypeAsync(CompanyType companyType);

    Task<(IReadOnlyList<CompanyType>, int)> GetMatchingAllCompanyTypesAsync(string? searchPhrase, string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);
}


