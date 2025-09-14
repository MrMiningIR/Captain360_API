using Capitan360.Domain.Entities.ManifestForms;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.ManifestForms;

public interface IManifestFormPeriodRepository
{
    Task<bool> CheckExistManifestFormPeriodCodeAsync(string manifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken);

    Task<int> CreateManifestFormPeriodAsync(ManifestFormPeriod manifestFormPeriod, CancellationToken cancellationToken);

    Task<ManifestFormPeriod?> GetManifestFormPeriodByIdAsync(int manifestFormPeriodId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task<IReadOnlyList<ManifestFormPeriod>?> GetManifestFormPeriodByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteManifestFormPeriodAsync(int manifestFormPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<ManifestFormPeriod>, int)> GetAllManifestFormPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<ManifestFormPeriod?> GetManifestFormPeriodByCodeAsync(string manifestFormPeriodCode, int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}
