using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Persistence;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Capitan360.Domain.Enums;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyInsuranceChargeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyInsuranceChargeRepository
{
    public async Task<int> CreateCompanyInsuranceChargeAsync(CompanyInsuranceCharge companyInsuranceCharge, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsuranceCharges.Add(companyInsuranceCharge);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsuranceCharge.Id;
    }
    public async Task<List<int>> CreateCompanyInsuranceChargeListAsync(List<CompanyInsuranceCharge> companyInsuranceChargeList, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsuranceCharges.AddRange(companyInsuranceChargeList);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsuranceChargeList.Select(p => p.Id).ToList();
    }

    public async Task<List<int>> UpdateByListAsync(List<CompanyInsuranceCharge> companyInsuranceChargeList, CancellationToken cancellationToken)
    {
        foreach (var item in companyInsuranceChargeList)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsuranceChargeList.Select(p => p.Id).ToList();
    }

    public async Task<List<int>> DeleteByListAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var prices = await dbContext.CompanyInsuranceCharges
        .Where(p => ids.Contains(p.Id))
        .ToListAsync(cancellationToken);
        foreach (var price in prices)
        {
            dbContext.Entry(price).Property("Deleted").CurrentValue = true; // Soft delete
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ids;
    }

    public void Delete(CompanyInsuranceCharge companyInsuranceCharge)
    {
        dbContext.Entry(companyInsuranceCharge).Property("Deleted").CurrentValue = true;
    }

    public async Task<CompanyInsuranceCharge?> GetCompanyInsuranceChargeById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsuranceCharges
            .Include(item => item.CompanyInsurance)
            .Include(item => item.CompanyInsuranceChargePayments)
            .Include(item => item.CompanyInsuranceChargePaymentContentTypes)
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyInsuranceCharge>, int)> GetAllCompanyInsuranceCharges(string? searchPhrase, int companyInsuranceId, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyInsuranceCharges
            .Include(item => item.CompanyInsurance)
            .Include(x=>x.CompanyInsuranceChargePayments)
            .Include(x=>x.CompanyInsuranceChargePaymentContentTypes)
            .AsNoTracking();

        if (companyInsuranceId != 0)
            baseQuery = baseQuery.Where(cic => cic.CompanyInsuranceId == companyInsuranceId);

        if (!string.IsNullOrEmpty(searchPhrase))
        {
            var searchPhraseLower = searchPhrase.ToLower().Trim();
            baseQuery = baseQuery.Where(cic => cic.Rate.ToString().ToLower().Contains(searchPhraseLower));
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyInsuranceCharge, object>>>
        {
            { nameof(CompanyInsuranceCharge.Rate), cic => cic.Rate },
            { nameof(CompanyInsuranceCharge.Value), cic => cic.Value },
            { nameof(CompanyInsuranceCharge.Settlement), cic => cic.Settlement }
        };

        sortBy ??= nameof(CompanyInsuranceCharge.Rate);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyInsuranceCharges = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyInsuranceCharges, totalCount);
    }

    public async Task<bool> CheckExistCompanyInsuranceCharge(Rate rate, int companyInsuranceId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsuranceCharges
            .AnyAsync(cic => cic.CompanyInsuranceId == companyInsuranceId && cic.Rate == rate, cancellationToken);
    }

    public async Task<List<CompanyInsuranceCharge>> GetExistingCompanyInsuranceCharge(List<int> ids, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsuranceCharges
.Where(p => ids.Contains(p.Id))
.ToListAsync(cancellationToken);
    }
}
