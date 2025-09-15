using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyDomesticPathChargeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticPathChargeRepository
{
    public async Task<List<int>> CreateByListAsync(List<CompanyDomesticPathCharge> items, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathCharges.AddRange(items);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return items.Select(p => p.Id).ToList();
    }

    public async Task DeleteByListAsync(List<int> priceIds, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyDomesticPathCharges
     .Where(p => priceIds.Contains(p.Id))
     .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> UpdateByListAsync(List<CompanyDomesticPathCharge> prices, CancellationToken cancellationToken)
    {
        foreach (var price in prices)
        {
            dbContext.Entry(price).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return prices.Select(p => p.Id).ToList();
    }

    public void Delete(CompanyDomesticPathCharge price)
    {
        dbContext.Entry(price).Property("Deleted").CurrentValue = true;
    }

    public async Task<(IReadOnlyList<CompanyDomesticPathCharge>, int)> GetAllCompanyDomesticPathCharge(string? searchPhrase, int companyDomesticPathId,
        int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        //Point
        var baseQuery = dbContext.CompanyDomesticPathCharges.AsNoTracking()
            .Include(x => x.CompanyDomesticPathChargeContentTypes
                .Where(item => companyDomesticPathId != 0 && item.CompanyDomesticPathId == companyDomesticPathId)


            )

            .Where(p => companyDomesticPathId == 0 || p.CompanyDomesticPathId == companyDomesticPathId);

        //.Where(p => searchPhraseLower == null ||
        //    p.Weight.ToString().Contains(searchPhraseLower) ||

        //    p.WeightType.ToString().ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPathCharge, object>>>
            {
                { nameof(CompanyDomesticPathCharge.Weight), p => p.Weight },
                { nameof(CompanyDomesticPathCharge.PriceDirect), p => p.PriceDirect },
                { nameof(CompanyDomesticPathCharge.WeightType), p => p.WeightType }
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

    public Task<bool> CheckExistPrice(int companyDomesticPathId, int weight, PathStructType pathStructType, WeightType weightType,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(CompanyDomesticPathCharge price, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathCharges.Add(price);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return price.Id;
    }

    public async Task<CompanyDomesticPathCharge?> GetCompanyCompanyDomesticPathChargeById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathCharges.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<CompanyDomesticPathCharge>> GetExistingStructPaths(List<int> ids, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathCharges
       .Where(p => ids.Contains(p.Id))
       .ToListAsync(cancellationToken);
    }
}