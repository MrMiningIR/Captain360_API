using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyDomesticPathStructPriceMunicipalAreasRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticPathStructPriceMunicipalAreasRepository
{
    public async Task<List<int>> Create(List<CompanyDomesticPathStructPriceMunicipalAreas> items, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathStructPriceMunicipalAreas.AddRange(items);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return items.Select(p => p.Id).ToList();
    }

    public async Task Delete(List<int> priceIds, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyDomesticPathStructPriceMunicipalAreas
            .Where(p => priceIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> Update(List<CompanyDomesticPathStructPriceMunicipalAreas> prices, CancellationToken cancellationToken)
    {
        foreach (var price in prices)
        {
            dbContext.Entry(price).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return prices.Select(p => p.Id).ToList();
    }
}