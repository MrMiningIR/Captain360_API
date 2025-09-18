using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyDomesticPathStructPriceMunicipalAreasRepository
{
    Task<List<int>> Create(List<CompanyDomesticPathStructPriceMunicipalArea> items, CancellationToken cancellationToken);
    Task Delete(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> Update(List<CompanyDomesticPathStructPriceMunicipalArea> prices,
        CancellationToken cancellationToken);

}