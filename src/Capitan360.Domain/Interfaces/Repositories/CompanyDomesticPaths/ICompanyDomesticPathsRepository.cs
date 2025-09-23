using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathsRepository
{
    Task<bool> CheckExistCompanyDomesticPathPathAsync(int companyId, int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId, CancellationToken cancellationToken);

    Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPath companyDomesticPath, CancellationToken cancellationToken);

    Task<CompanyDomesticPath?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticPathAsync(int companyDomesticPathId);

    Task<(IReadOnlyList<CompanyDomesticPath>, int)> GetAllCompanyDomesticPathsAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int active, int sourceCityId, int destinationCityId, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPath>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}