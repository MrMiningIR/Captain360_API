using System.Linq.Expressions;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.DomesticWaybillEntity;
using Capitan360.Domain.Repositories.DomesticWaybillRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.DomesticWaybillImpl;

public class DomesticWaybillPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IDomesticWaybillPeriodRepository
{
    public async Task<bool> CheckExistDomesticWaybillPeriodCodeAsync(string domesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybillPeriods.AnyAsync(item => (currentDomesticWaybillPeriodId == null || item.Id != currentDomesticWaybillPeriodId) && item.CompanyId == companyId && item.Code.ToLower() == domesticWaybillPeriodCode.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> CreateDomesticWaybillPeriodAsync(DomesticWaybillPeriod domesticWaybillPeriod, CancellationToken cancellationToken)
    {
        dbContext.DomesticWaybillPeriods.Add(domesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return domesticWaybillPeriod.Id;
    }

    public async Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByIdAsync(int domesticWaybillPeriodId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.SingleOrDefaultAsync(item =>item.Id == domesticWaybillPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<DomesticWaybillPeriod>?> GetDomesticWaybillPeriodByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.Where(item =>item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteDomesticWaybillPeriodAsync(int domesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        dbContext.DomesticWaybillPeriods.Remove(await dbContext.DomesticWaybillPeriods.SingleOrDefaultAsync(item => item.Id == domesticWaybillPeriodId, cancellationToken));
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<DomesticWaybillPeriod>, int)> GetAllDomesticWaybillPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.DomesticWaybillPeriods.AsNoTracking()
                                                        .Where(item => searchPhraseLower == null || item.Code.ToLower().Contains(searchPhraseLower) || item.Description.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(item =>item.Active),
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

    public async Task<DomesticWaybillPeriod?> GetDomesticWaybillPeriodByCodeAsync(string domesticWaybillPeriodCode, int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybillPeriod> query = dbContext.DomesticWaybillPeriods;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.SingleOrDefaultAsync(item =>item.Code.ToLower() == domesticWaybillPeriodCode.ToLower() &&item.CompanyId == companyId, cancellationToken);
    }
}
