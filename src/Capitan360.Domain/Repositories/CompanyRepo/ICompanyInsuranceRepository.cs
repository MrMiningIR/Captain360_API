using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyInsuranceRepository
{
    Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken);
    void Delete(CompanyInsurance companyInsurance);
    Task<CompanyInsurance?> GetCompanyInsuranceById(int id, CancellationToken cancellationToken, bool track = false);
    Task<(IReadOnlyList<CompanyInsurance>, int)> GetMatchingAllCompanyInsurances(string? searchPhrase, int companyTypeId, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistCompanyInsuranceName(string name, int companyTypeId, int companyId, CancellationToken cancellationToken);

}