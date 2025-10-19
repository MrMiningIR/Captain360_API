using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticWaybills;

public class CompanyDomesticWaybillPackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticWaybillPackageTypeRepository
{
    public async Task<int> IssueCompanyDomesticWaybillPackageTypeAsync(CompanyDomesticWaybillPackageType companyDomesticWaybillPackageType, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticWaybillPackageTypes.Add(companyDomesticWaybillPackageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticWaybillPackageType.Id;
    }

    public async Task<CompanyDomesticWaybillPackageType?> GetCompanyDomesticWaybillPackageTypeByIdAsync(int companyDomesticWaybillPackageTypeId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPackageType> query = dbContext.CompanyDomesticWaybillPackageTypes;

        if (loadData)
        {
            query = query.Include(item => item.CompanyContentType);
            query = query.Include(item => item.CompanyPackageType);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPackageTypeId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyDomesticWaybillPackageType>?> GetCompanyDomesticWaybillPackageTypeByCompanyDomesticWaybillIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPackageType> query = dbContext.CompanyDomesticWaybillPackageTypes.Include(item => item.CompanyContentType)
                                                                                                          .Include(item => item.CompanyPackageType)
                                                                                                          .AsNoTracking();

        return await query.Where(item => item.CompanyDomesticWaybillId == companyDomesticWaybillId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyDomesticWaybillPackageTypeAsync(int companyDomesticWaybillPackageTypeId, CancellationToken cancellationToken)
    {
        var companyDomesticWaybillPackageType = await dbContext.CompanyDomesticWaybillPackageTypes.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPackageTypeId, cancellationToken);
        if (companyDomesticWaybillPackageType == null)
            return;

        dbContext.CompanyDomesticWaybillPackageTypes.Remove(companyDomesticWaybillPackageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCompanyDomesticWaybillPackageTypeByByCompanyDomesticWaybillIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken)
    {
        var companyDomesticWaybillPackageTypes = await dbContext.CompanyDomesticWaybillPackageTypes.Where(item => item.CompanyDomesticWaybillId == companyDomesticWaybillId)
                                                                                                   .ToListAsync(cancellationToken);

        dbContext.CompanyDomesticWaybillPackageTypes.RemoveRange(companyDomesticWaybillPackageTypes);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}