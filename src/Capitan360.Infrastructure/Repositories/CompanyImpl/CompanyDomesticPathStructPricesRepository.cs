using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Capitan360.Domain.Enums;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyDomesticPathStructPricesRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyDomesticPathStructPricesRepository
{
    public async Task<int> CreateCompanyDomesticPathStructPriceAsync(CompanyDomesticPathStructPrices price, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathStructPrices.Add(price);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return price.Id;
    }

    public async Task<List<int>> CreateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrices> items, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathStructPrices.AddRange(items);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return items.Select(p => p.Id).ToList();
    }

    public async Task DeleteCompanyDomesticPathStructPricesAsync(List<int> priceIds, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyDomesticPathStructPrices
            .Where(p => priceIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Delete(CompanyDomesticPathStructPrices price)
    {
        dbContext.Entry(price).Property("Deleted").CurrentValue = true;
    }

    public async Task<CompanyDomesticPathStructPrices?> GetCompanyDomesticPathStructPriceById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathStructPrices.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<int> GetCountOfExistingStructPaths(List<int> ids, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathStructPrices
             .Where(p => ids.Contains(p.Id))
             .CountAsync(cancellationToken);
    }

    public async Task<List<CompanyDomesticPathStructPrices>> GetExistingStructPaths(List<int> ids,
        CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathStructPrices
             .Where(p => ids.Contains(p.Id))
             .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> UpdateCompanyDomesticPathStructPriceAsync(List<CompanyDomesticPathStructPrices> prices,
        CancellationToken cancellationToken)
    {
        foreach (var price in prices)
        {
            dbContext.Entry(price).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return prices.Select(p => p.Id).ToList();
    }

    public async Task<(IReadOnlyList<CompanyDomesticPathStructPrices>, int)>
        GetMatchingAllCompanyDomesticPathStructPrice(string? searchPhrase, int companyDomesticPathId, int pathStruct,
            int pageSize, int pageNumber, string? sortBy,
            SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
//Point
        var baseQuery = dbContext.CompanyDomesticPathStructPrices.AsNoTracking()
            .Include(x => x.CompanyDomesticPathStructPriceMunicipalAreas
                .Where(a => companyDomesticPathId != 0 && a.CompanyDomesticPathId == companyDomesticPathId)
            
             .Where(a=>pathStruct==0 || a.PathStructType==(PathStructType)pathStruct)
            
            
            )

            .Where(p => companyDomesticPathId == 0 || p.CompanyDomesticPathId == companyDomesticPathId)
            .Where(x=>pathStruct==0 || x.PathStructType==(PathStructType)pathStruct)
            .Where(p => searchPhraseLower == null ||
                p.Weight.ToString().Contains(searchPhraseLower) ||
                p.PathStructType.ToString().ToLower().Contains(searchPhraseLower) ||
                p.WeightType.ToString().ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPathStructPrices, object>>>
            {
                { nameof(CompanyDomesticPathStructPrices.Weight), p => p.Weight },
                { nameof(CompanyDomesticPathStructPrices.PathStructType), p => p.PathStructType },
                { nameof(CompanyDomesticPathStructPrices.WeightType), p => p.WeightType }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var items = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<bool> CheckExistPrice(int companyDomesticPathId, int weight, PathStructType pathStructType, WeightType weightType, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathStructPrices.AnyAsync(
            p => p.CompanyDomesticPathId == companyDomesticPathId &&
                 p.Weight == weight &&
                 p.PathStructType == pathStructType &&
                 p.WeightType == weightType,
            cancellationToken);
    }




}