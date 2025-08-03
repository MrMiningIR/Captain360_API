using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyRepository
{
    Task<int> CreateCompanyAsync(Company company, CancellationToken cancellationToken);
    void Delete(Company company);
    Task<IReadOnlyList<Company>> GetAllCompanies(int companyTypeId, CancellationToken cancellationToken);
    Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken, bool tracked, int userCompanyTypeId = 0
      );



    Task<(IReadOnlyList<Company>, int)> GetMatchingAllCompanies(string? searchPhrase, int companyTypeId, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<List<int>> GetCompaniesIdByCompanyTypeId(int companyTypeId, CancellationToken cancellationToken);

    Task<bool> ValidateCompanyDataWithUserCompanyType(int userCompanyType, int companyId,
        CancellationToken cancellationToken);
}