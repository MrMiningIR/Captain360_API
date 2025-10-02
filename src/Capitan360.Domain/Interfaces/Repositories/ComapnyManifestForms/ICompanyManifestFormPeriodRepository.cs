using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;

public interface ICompanyManifestFormPeriodRepository
{
    Task<bool> CheckExistCompanyManifestFormPeriodCodeAsync(string companyManifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken);

    Task<int> CreateCompanyManifestFormPeriodAsync(CompanyManifestFormPeriod companyManifestFormPeriod, CancellationToken cancellationToken);

    Task<CompanyManifestFormPeriod?> GetCompanyManifestFormPeriodByIdAsync(int companyManifestFormPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyManifestFormPeriod>?> GetCompanyManifestFormPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task DeleteCompanyManifestFormPeriodAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyManifestFormPeriodGetAllDto>, int)> GetAllCompanyManifestFormPeriodsAsync(string searchPhrase, string? sortBy, int companyId, int active, int hasReadyForm, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyManifestFormPeriod?> GetCompanyManifestFormPeriodByCodeAsync(string companyManifestFormPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}
