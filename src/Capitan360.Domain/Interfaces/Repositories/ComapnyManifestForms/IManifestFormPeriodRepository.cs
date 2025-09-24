using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;

public interface IManifestFormPeriodRepository
{
    Task<bool> CheckExistManifestFormPeriodCodeAsync(string companyManifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken);

    Task<int> CreateManifestFormPeriodAsync(CompanyManifestFormPeriod companyManifestFormPeriod, CancellationToken cancellationToken);

    Task<CompanyManifestFormPeriod?> GetManifestFormPeriodByIdAsync(int companyManifestFormPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyManifestFormPeriod>?> GetManifestFormPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task DeleteManifestFormPeriodAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyManifestFormPeriod>, int)> GetAllManifestFormPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyManifestFormPeriod?> GetManifestFormPeriodByCodeAsync(string companyManifestFormPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}
