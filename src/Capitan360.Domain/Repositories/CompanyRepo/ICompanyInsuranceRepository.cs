using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyInsuranceRepository
{
    Task<bool> CheckExistCompanyInsuranceNameAsync(string companyInsuranceName, int? currentCompanyInsuranceId, int companyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyInsuranceCodeAsync(string companyInsuranceCode, int? currentCompanyInsuranceId, int companyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken);

    Task<CompanyInsurance?> GetCompanyInsuranceByIdAsync(int companyInsuranceId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyInsurance>?> GetCompanyInsuranceByCompanyIdAsync(int companyInsuranceCompanyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyInsuranceAsync(CompanyInsurance companyInsurance);

    Task<(IReadOnlyList<CompanyInsurance>, int)> GetMatchingAllCompanyInsurancesAsync(string? searchPhrase, string? sortBy, int companyId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

}