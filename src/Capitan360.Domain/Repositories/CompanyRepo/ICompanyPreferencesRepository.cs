using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyPreferencesRepository
{
    Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken);
    void Delete(CompanyPreferences companyPreferences, string userId);

    Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int id, bool tracked,
        CancellationToken cancellationToken);
    Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int id, bool tracked, CancellationToken cancellationToken);


    Task<(IReadOnlyList<CompanyPreferences>, int)> GetAllCompanyPreferencesAsync(string? searchPhrase, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}