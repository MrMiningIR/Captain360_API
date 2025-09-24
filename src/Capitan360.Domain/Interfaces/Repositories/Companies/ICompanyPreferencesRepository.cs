using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Companies;

public interface ICompanyPreferencesRepository
{
    Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken);

    Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int companyPreferencesId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyPreferencesAsync(int companyPreferencesId);

    Task<(IReadOnlyList<CompanyPreferences>, int)> GetAllCompanyPreferencesAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}