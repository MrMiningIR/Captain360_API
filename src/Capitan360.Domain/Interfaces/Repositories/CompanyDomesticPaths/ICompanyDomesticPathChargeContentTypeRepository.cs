using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathChargeContentTypeRepository
{
    Task<List<int>> Create(List<CompanyDomesticPathChargeContentType> contentTypes, CancellationToken cancellationToken);
    Task Delete(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> Update(List<CompanyDomesticPathChargeContentType> contentTypes,
        CancellationToken cancellationToken);

    Task<List<int>> ShouldDeleteAsReference(List<int> companyDomesticPathChargeIds, CancellationToken cancellationToken);
}