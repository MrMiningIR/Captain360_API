using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.DomesticWaybills;

public interface IDomesticWaybillPeriodRepository
{
    Task<bool> CheckExistDomesticWaybillPeriodCodeAsync(string domesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<int> CreateDomesticWaybillPeriodAsync(DomesticWaybillPeriod domesticWaybillPeriod, CancellationToken cancellationToken);

    Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByIdAsync(int domesticWaybillPeriodId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task<IReadOnlyList<DomesticWaybillPeriod>?> GetDomesticWaybillPeriodByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteDomesticWaybillPeriodAsync(int domesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<DomesticWaybillPeriod>, int)> GetAllDomesticWaybillPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByCodeAsync(string domesticWaybillPeriodCode, int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}
