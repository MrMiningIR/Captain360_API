using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyPreferencesRepository
{
    Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken);

    Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int companyPreferencesId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyPreferencesAsync(CompanyPreferences companyPreferences);

    Task<(IReadOnlyList<CompanyPreferences>, int)> GetMatchingAllCompanyPreferencesAsync(string? searchPhrase, string? sortBy, int companyTypeId, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}