using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyInsuranceChargePaymentContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) :
    ICompanyInsuranceChargePaymentContentTypeRepository
{
  

    public async Task<List<int>> CreateInsuranceChargePayment(List<CompanyInsuranceChargePaymentContentType> chargePaymentsContentTypes, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsurancesChargePaymentContentTypes.AddRange(chargePaymentsContentTypes);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return chargePaymentsContentTypes.Select(p => p.Id).ToList();
    }

    public async Task Delete(List<int> ids, CancellationToken cancellationToken)
    {
        var deleteItems = await dbContext.CompanyInsurancesChargePaymentContentTypes
      .Where(p => ids.Contains(p.Id))
      .ToListAsync(cancellationToken);
        foreach (var item in deleteItems)
        {
            dbContext.Entry(item).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> Update(List<CompanyInsuranceChargePaymentContentType> chargePaymentContentTypes, CancellationToken cancellationToken)
    {
        foreach (var item in chargePaymentContentTypes)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return chargePaymentContentTypes.Select(p => p.Id).ToList();
    }
    public async Task<List<int>> ChargePaymentContentTypeReferenceIds(List<int> companyInsuranceChargeContentTypeIds, CancellationToken cancellationToken)
    {
        var referenceIds = new List<int>();

        foreach (var companyInsuranceChargeContentTypeId in companyInsuranceChargeContentTypeIds)
        {
            var targetIds = await dbContext
                .CompanyInsurancesChargePaymentContentTypes.AsNoTracking()
                .Where(x => x.CompanyInsuranceChargeId == companyInsuranceChargeContentTypeId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            referenceIds.AddRange(targetIds);
        }



        return referenceIds.Distinct().ToList();
    }

}