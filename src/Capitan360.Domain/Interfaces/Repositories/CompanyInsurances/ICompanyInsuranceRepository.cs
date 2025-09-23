using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;

public interface ICompanyInsuranceRepository
{
    Task<bool> CheckExistCompanyInsuranceNameAsync(string companyInsuranceName, int companyId, int? currentCompanyInsuranceId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyInsuranceCodeAsync(string companyInsuranceCode, int companyId, int? currentCompanyInsuranceId, CancellationToken cancellationToken);

    Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken);

    Task<CompanyInsurance?> GetCompanyInsuranceByIdAsync(int companyInsuranceId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyInsurance>?> GetCompanyInsuranceByCompanyIdAsync(int companyInsuranceCompanyId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyInsuranceAsync(int companyInsuranceId);

    Task<(IReadOnlyList<CompanyInsurance>, int)> GetAllCompanyInsurancesAsync(string? searchPhrase, string? sortBy, int companyId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyInsurance?> GetCompanyInsuranceByCodeAsync(string companyInsuranceCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}