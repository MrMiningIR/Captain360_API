using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;

public interface ICompanyDomesticWaybillPackageTypeRepository
{
    Task<int> IssueCompanyDomesticWaybillPackageTypeAsync(CompanyDomesticWaybillPackageType companyDomesticWaybillPackageType, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybillPackageType?> GetCompanyDomesticWaybillPackageTypeByIdAsync(int companyDomesticWaybillPackageTypeId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticWaybillPackageType>?> GetCompanyDomesticWaybillPackageTypeByCompanyDomesticWaybillIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticWaybillPackageTypeAsync(int companyDomesticWaybillPackageTypeId, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticWaybillPackageTypeByByCompanyDomesticWaybillIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);
}
