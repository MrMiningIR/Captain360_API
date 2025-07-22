using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyInsuranceChargeRepository
{
    Task<int> CreateCompanyInsuranceChargeAsync(CompanyInsuranceCharge companyInsuranceCharge, CancellationToken cancellationToken);

    Task<List<int>> CreateCompanyInsuranceChargeListAsync(List<CompanyInsuranceCharge> companyInsuranceChargeList,
        CancellationToken cancellationToken);




    Task<List<int>> UpdateByListAsync(List<CompanyInsuranceCharge> companyInsuranceChargeList,
        CancellationToken cancellationToken);


     Task<List<int>> DeleteByListAsync(List<int> ids, CancellationToken cancellationToken);

    void Delete(CompanyInsuranceCharge companyInsuranceCharge);
    Task<CompanyInsuranceCharge?> GetCompanyInsuranceChargeById(int id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<CompanyInsuranceCharge>, int)> GetMatchingAllCompanyInsuranceCharges(string? searchPhrase, int companyInsuranceId, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistCompanyInsuranceCharge(Rate rate, int companyInsuranceId, CancellationToken cancellationToken);
    Task<List<CompanyInsuranceCharge>> GetExistingCompanyInsuranceCharge(List<int> ids, CancellationToken cancellationToken);




}