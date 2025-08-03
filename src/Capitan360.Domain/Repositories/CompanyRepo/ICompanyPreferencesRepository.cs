using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyPreferencesRepository
{
    Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken);
    void Delete(CompanyPreferences companyPreferences, string userId);
    Task<IReadOnlyList<CompanyPreferences>> GetAllCompanyPreferences(CancellationToken cancellationToken);
    Task<CompanyPreferences?> GetCompanyPreferencesByCompanyId(int id, CancellationToken cancellationToken,
        bool track);
    Task<CompanyPreferences?> GetCompanyPreferencesById(int id, CancellationToken cancellationToken,
        bool track);


    Task<(IReadOnlyList<CompanyPreferences>, int)> GetMatchingAllCompanyPreferences(string? searchPhrase, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}