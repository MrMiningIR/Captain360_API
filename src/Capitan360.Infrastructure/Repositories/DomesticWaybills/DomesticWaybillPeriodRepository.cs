using System.Linq.Expressions;
using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.DomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.DomesticWaybills;

public class DomesticWaybillPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IDomesticWaybillPeriodRepository
{
    public async Task<bool> CheckExistDomesticWaybillPeriodCodeAsync(string domesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybillPeriods.AnyAsync(item => item.Code.ToLower() == domesticWaybillPeriodCode.ToLower().Trim() && item.CompanyId == companyId && (currentDomesticWaybillPeriodId == null || item.Id != currentDomesticWaybillPeriodId), cancellationToken);
    }

    public async Task<int> CreateDomesticWaybillPeriodAsync(DomesticWaybillPeriod domesticWaybillPeriod, CancellationToken cancellationToken)
    {
        dbContext.DomesticWaybillPeriods.Add(domesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return domesticWaybillPeriod.Id;
    }

    public async Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByIdAsync(int domesticWaybillPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == domesticWaybillPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<DomesticWaybillPeriod>?> GetDomesticWaybillPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteDomesticWaybillPeriodAsync(int domesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        var domesticWaybillPeriod = await dbContext.DomesticWaybillPeriods.SingleOrDefaultAsync(item => item.Id == domesticWaybillPeriodId, cancellationToken);
        if (domesticWaybillPeriod == null)
            return;

        dbContext.DomesticWaybillPeriods.Remove(domesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<DomesticWaybillPeriod>, int)> GetAllDomesticWaybillPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.DomesticWaybillPeriods.AsNoTracking()
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

        var columnsSelector = new Dictionary<string, Expression<Func<DomesticWaybillPeriod, object>>>
    {
        { nameof(DomesticWaybillPeriod.Code), item => item.Code},
        { nameof(DomesticWaybillPeriod.Active), item => item.Active},
        { nameof(DomesticWaybillPeriod.StartNumber), item => item.StartNumber}
    };

        sortBy ??= nameof(DomesticWaybillPeriod.Code);

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

    public async Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByCodeAsync(string domesticWaybillPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == domesticWaybillPeriodCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
