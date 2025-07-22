using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyDomesticPathsRepository
{
    Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath, CancellationToken cancellationToken);
    void Delete(CompanyDomesticPaths companyDomesticPath);
    Task<CompanyDomesticPaths?> GetCompanyDomesticPathById(int id, CancellationToken cancellationToken,
        bool track = false);
    Task<(IReadOnlyList<CompanyDomesticPaths>, int)> GetMatchingAllCompanyDomesticPaths(string? searchPhrase,
        int companyId, int pageSize, int pageNumber, int status, string? sortBy,
        int? sourceCountryId, int? sourceProvinceId, int? sourceCityId, int? destinationCountryId,
        int? destinationProvinceId, int? destinationCityId, SortDirection sortDirection,
        CancellationToken cancellationToken);
    Task<bool> CheckExistPath(int sourceCityId, int destinationCityId, int companyId, CancellationToken cancellationToken);
    Task<CompanyDomesticPaths?> CheckExistPath(int domesticPathId, CancellationToken cancellationToken);

}