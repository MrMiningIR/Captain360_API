using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyDomesticPathStructPricesRepository
{
    Task<List<int>> CreateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrices> items, CancellationToken cancellationToken);
    Task DeleteCompanyDomesticPathStructPricesAsync(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> UpdateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrices> prices,
        CancellationToken cancellationToken);
    void Delete(CompanyDomesticPathStructPrices price);
    Task<(IReadOnlyList<CompanyDomesticPathStructPrices>, int)> GetAllCompanyDomesticPathStructPrice(
        string? searchPhrase, int companyDomesticPathId,int pathStruct, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistPrice(int companyDomesticPathId, int weight, PathStructType pathStructType, WeightType weightType, CancellationToken cancellationToken);
    Task<int> CreateCompanyDomesticPathStructPriceAsync(CompanyDomesticPathStructPrices price, CancellationToken cancellationToken);
    Task<CompanyDomesticPathStructPrices?> GetCompanyDomesticPathStructPriceById(int id, CancellationToken cancellationToken);
    Task<List<CompanyDomesticPathStructPrices>> GetExistingStructPaths(List<int> ids,
        CancellationToken cancellationToken);

    Task<int> GetCountOfExistingStructPaths(List<int> ids, CancellationToken cancellationToken);
    

}