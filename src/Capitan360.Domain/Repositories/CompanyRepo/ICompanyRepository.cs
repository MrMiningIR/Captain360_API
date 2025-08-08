using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyRepository
{
    Task<int> CreateCompanyAsync(Company company, CancellationToken cancellationToken);
    void Delete(Company company);
    Task<IReadOnlyList<Company>> GetAllCompaniesAsync(int companyTypeId, CancellationToken cancellationToken);
    Task<Company?> GetCompanyByIdAsync(int id, CancellationToken cancellationToken, bool tracked, int userCompanyTypeId = 0
      );


    Task<bool> CheckExistCompanyNameAsync(string companyName, int? currentCompanyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyCodeAsync(string companyCode, int? currentCompanyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyIsParentCompanyAsync(int companyTypeId, int? currentCompanyId, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Company>, int)> GetAllCompaniesAsync(string? searchPhrase, int companyTypeId, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<List<int>> GetCompaniesIdByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<bool> ValidateCompanyDataWithUserCompanyTypeAsync(int userCompanyType, int companyId,
        CancellationToken cancellationToken);
}