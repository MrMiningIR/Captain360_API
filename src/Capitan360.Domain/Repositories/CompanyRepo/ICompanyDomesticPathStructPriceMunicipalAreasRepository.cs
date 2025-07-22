using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyDomesticPathStructPriceMunicipalAreasRepository
{
    Task<List<int>> Create(List<CompanyDomesticPathStructPriceMunicipalAreas> items, CancellationToken cancellationToken);
    Task Delete(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> Update(List<CompanyDomesticPathStructPriceMunicipalAreas> prices,
        CancellationToken cancellationToken);

}