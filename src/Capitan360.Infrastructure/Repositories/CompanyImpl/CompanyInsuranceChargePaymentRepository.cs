using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyInsuranceChargePaymentRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyInsuranceChargePaymentRepository
{
    public async Task<List<int>> CreateInsuranceChargePayment(List<CompanyInsuranceChargePayment> chargePayments, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsurancesChargePayments.AddRange(chargePayments);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return chargePayments.Select(p => p.Id).ToList();
    }

    public async Task Delete(List<int> ids, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyInsurancesChargePayments
     .Where(p => ids.Contains(p.Id))
     .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> ChargePaymentReferenceIds(List<int> companyInsuranceChargeIds, CancellationToken cancellationToken)
    {
       
        
        var referenceIds = new List<int>();

        foreach (var companyInsuranceChargeId in companyInsuranceChargeIds)
        {
            var targetIds = await dbContext
                .CompanyInsurancesChargePayments.AsNoTracking()
                .Where(x => x.CompanyInsuranceChargeId == companyInsuranceChargeId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            referenceIds.AddRange(targetIds);
        }



        return referenceIds.Distinct().ToList();
    }

    public async Task<List<int>> Update(List<CompanyInsuranceChargePayment> chargePayments, CancellationToken cancellationToken)
    {
        foreach (var item in chargePayments)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return chargePayments.Select(p => p.Id).ToList();
    }
}