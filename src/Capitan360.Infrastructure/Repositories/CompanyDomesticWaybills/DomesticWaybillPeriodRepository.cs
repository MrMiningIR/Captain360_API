using System.Linq.Expressions;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticWaybills;

public class CompanyDomesticWaybillPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticWaybillPeriodRepository
{
    public async Task<bool> CheckExistDomesticWaybillPeriodCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybillPeriods.AnyAsync(item => item.Code.ToLower() == companyDomesticWaybillPeriodCode.ToLower().Trim() && item.CompanyId == companyId && (currentDomesticWaybillPeriodId == null || item.Id != currentDomesticWaybillPeriodId), cancellationToken);
    }

    public async Task<int> CreateDomesticWaybillPeriodAsync(CompanyDomesticWaybillPeriod companyDomesticWaybillPeriod, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticWaybillPeriods.Add(companyDomesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticWaybillPeriod.Id;
    }

    public async Task<CompanyDomesticWaybillPeriod?> GetDomesticWaybillPeriodByIdAsync(int companyDomesticWaybillPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyDomesticWaybillPeriod>?> GetDomesticWaybillPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteDomesticWaybillPeriodAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        var companyDomesticWaybillPeriod = await dbContext.CompanyDomesticWaybillPeriods.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPeriodId, cancellationToken);
        if (companyDomesticWaybillPeriod == null)
            return;

        dbContext.CompanyDomesticWaybillPeriods.Remove(companyDomesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyDomesticWaybillPeriod>, int)> GetAllDomesticWaybillPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyDomesticWaybillPeriods.AsNoTracking()
                                                        .Where(item => searchPhraseLower == null || item.Code.ToLower().Contains(searchPhraseLower) || item.Description.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        if (companyId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticWaybillPeriod, object>>>
    {
        { nameof(CompanyDomesticWaybillPeriod.Code), item => item.Code},
        { nameof(CompanyDomesticWaybillPeriod.Active), item => item.Active},
        { nameof(CompanyDomesticWaybillPeriod.StartNumber), item => item.StartNumber}
    };

        sortBy ??= nameof(CompanyDomesticWaybillPeriod.Code);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var packageTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (packageTypes, totalCount);
    }

    public async Task<CompanyDomesticWaybillPeriod?> GetDomesticWaybillPeriodByCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyDomesticWaybillPeriodCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
