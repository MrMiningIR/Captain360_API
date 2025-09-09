using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyRepository
{
    Task<List<int>> GetCompaniesIdByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyNameAsync(string companyName, int? currentCompanyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyCodeAsync(string companyCode, int? currentCompanyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyIsParentCompanyAsync(int companyTypeId, int? currentCompanyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyAsync(Company company, CancellationToken cancellationToken);

    Task<Company?> GetCompanyByIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyAsync(Company company);

    Task<(IReadOnlyList<Company>, int)> GetMatchingAllCompaniesAsync(string? searchPhrase, string? sortBy, int CompanyId, int companyTypeId, int cityId, int isParentCompany, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<IReadOnlyList<Company>> GetAllCompaniesAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<bool> ValidateCompanyDataWithUserCompanyTypeAsync(int userCompanyType, int companyId,
        CancellationToken cancellationToken);
}