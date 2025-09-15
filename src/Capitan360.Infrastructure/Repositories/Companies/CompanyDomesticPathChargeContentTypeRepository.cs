using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyDomesticPathChargeContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticPathChargeContentTypeRepository
{
    public async Task<List<int>> Create(List<CompanyDomesticPathChargeContentType> contentTypes, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathChargeContentTypes.AddRange(contentTypes);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return contentTypes.Select(p => p.Id).ToList();
    }

    public async Task Delete(List<int> priceIds, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyDomesticPathChargeContentTypes
            .Where(p => priceIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> Update(List<CompanyDomesticPathChargeContentType> contentTypes, CancellationToken cancellationToken)
    {
        foreach (var item in contentTypes)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return contentTypes.Select(p => p.Id).ToList();
    }

    public async Task<List<int>> ShouldDeleteAsReference(List<int> companyDomesticPathChargeIds, CancellationToken cancellationToken)
    {
        var referenceIds = new List<int>();

        foreach (var item in companyDomesticPathChargeIds)
        {
            var targetIds = await dbContext
                .CompanyDomesticPathChargeContentTypes
                .Where(x => x.CompanyDomesticPathChargeId == item)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            referenceIds.AddRange(targetIds);
        }



        return referenceIds.Distinct().ToList();
    }
}