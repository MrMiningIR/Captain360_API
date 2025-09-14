using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyDomesticPathChargeContentTypeRepository
{
    Task<List<int>> Create(List<CompanyDomesticPathChargeContentType> contentTypes, CancellationToken cancellationToken);
    Task Delete(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> Update(List<CompanyDomesticPathChargeContentType> contentTypes,
        CancellationToken cancellationToken);

    Task<List<int>> ShouldDeleteAsReference(List<int>  companyDomesticPathChargeIds, CancellationToken cancellationToken);
}