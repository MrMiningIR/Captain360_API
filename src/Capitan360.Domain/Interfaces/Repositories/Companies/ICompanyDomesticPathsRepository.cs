using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyDomesticPathsRepository
{
    Task<bool> CheckExistCompanyDomesticPathPathAsync(int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId, int companyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath, CancellationToken cancellationToken);

    Task<CompanyDomesticPaths?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath);

    Task<(IReadOnlyList<CompanyDomesticPaths>, int)> GetAllCompanyDomesticPathsAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int active, int sourceCityId, int destinationCityId, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPaths>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}