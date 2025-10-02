using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathRepository
{
    Task<bool> CheckExistCompanyDomesticPathAsync(int companyId, int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId, CancellationToken cancellationToken);

    Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPath companyDomesticPath, CancellationToken cancellationToken);

    Task<CompanyDomesticPath?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool loadDataCompanyData, bool loadDataSourceData, bool loadDataDestinationData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticPathAsync(int companyDomesticPathId);

    Task<(IReadOnlyList<CompanyDomesticPath>, int)> GetAllCompanyDomesticPathsAsync(string searchPhrase, string? sortBy, int companyId, int active, int sourceCityId, int destinationCityId, bool loadDataCompanyData, bool loadDataSourceData, bool loadDataDestinationData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPath>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, CancellationToken cancellationToken);
}






