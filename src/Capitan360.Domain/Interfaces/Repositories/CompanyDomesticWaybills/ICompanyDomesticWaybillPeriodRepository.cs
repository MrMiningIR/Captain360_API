using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;

public interface ICompanyDomesticWaybillPeriodRepository
{
    Task<bool> CheckExistDomesticWaybillPeriodCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<int> CreateDomesticWaybillPeriodAsync(CompanyDomesticWaybillPeriod companyDomesticWaybillPeriod, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybillPeriod?> GetDomesticWaybillPeriodByIdAsync(int companyDomesticWaybillPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticWaybillPeriod>?> GetDomesticWaybillPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task DeleteDomesticWaybillPeriodAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyDomesticWaybillPeriod>, int)> GetAllDomesticWaybillPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybillPeriod?> GetDomesticWaybillPeriodByCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}
