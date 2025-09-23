using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathStructPriceMunicipalAreasRepository
{
    Task<List<int>> Create(List<CompanyDomesticPathStructPriceMunicipalArea> items, CancellationToken cancellationToken);
    Task Delete(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> Update(List<CompanyDomesticPathStructPriceMunicipalArea> prices,
        CancellationToken cancellationToken);

}