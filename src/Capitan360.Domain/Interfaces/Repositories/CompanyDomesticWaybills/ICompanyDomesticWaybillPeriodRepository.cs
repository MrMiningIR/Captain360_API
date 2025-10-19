using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Dtos;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;

public interface ICompanyDomesticWaybillPeriodRepository
{
    Task<bool> CheckExistCompanyDomesticWaybillPeriodCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<int> CreateCompanyDomesticWaybillPeriodAsync(CompanyDomesticWaybillPeriod companyDomesticWaybillPeriod, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybillPeriod?> GetCompanyDomesticWaybillPeriodByIdAsync(int companyDomesticWaybillPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticWaybillPeriod>?> GetCompanyDomesticWaybillPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticWaybillPeriodAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyDomesticWaybillPeriodGetAllDto>, int)> GetAllCompanyDomesticWaybillPeriodsAsync(string searchPhrase, string? sortBy, int companyId, int active, int hasReadyForm, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybillPeriod?> GetCompanyDomesticWaybillPeriodByCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}