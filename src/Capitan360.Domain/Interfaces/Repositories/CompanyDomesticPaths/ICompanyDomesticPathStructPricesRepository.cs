using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathStructPricesRepository
{
    Task<List<int>> CreateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrice> items, CancellationToken cancellationToken);
    Task DeleteCompanyDomesticPathStructPricesAsync(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> UpdateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrice> prices,
        CancellationToken cancellationToken);
    void Delete(CompanyDomesticPathStructPrice price);
    Task<(IReadOnlyList<CompanyDomesticPathStructPrice>, int)> GetAllCompanyDomesticPathStructPrice(
        string searchPhrase, int companyDomesticPathId, int pathStruct, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistPrice(int companyDomesticPathId, int weight, PathStructType pathStructType, WeightType weightType, CancellationToken cancellationToken);
    Task<int> CreateCompanyDomesticPathStructPriceAsync(CompanyDomesticPathStructPrice price, CancellationToken cancellationToken);
    Task<CompanyDomesticPathStructPrice?> GetCompanyDomesticPathStructPriceById(int id, CancellationToken cancellationToken);
    Task<List<CompanyDomesticPathStructPrice>> GetExistingStructPaths(List<int> ids,
        CancellationToken cancellationToken);

    Task<int> GetCountOfExistingStructPaths(List<int> ids, CancellationToken cancellationToken);


}