using System.Linq.Expressions;
using Capitan360.Domain.Entities.ManifestForms;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ManifestForms;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.ManifestForms;

public class ManifestFormPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IManifestFormPeriodRepository
{
    public async Task<bool> CheckExistManifestFormPeriodCodeAsync(string manifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestFormPeriods.AnyAsync(item => item.CompanyId == companyId && item.Code.ToLower() == manifestFormPeriodCode.ToLower().Trim() && (currentManifestFormPeriodId == null || item.Id != currentManifestFormPeriodId), cancellationToken);
    }

    public async Task<int> CreateManifestFormPeriodAsync(ManifestFormPeriod manifestFormPeriod, CancellationToken cancellationToken)
    {
        dbContext.ManifestFormPeriods.Add(manifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return manifestFormPeriod.Id;
    }

    public async Task<ManifestFormPeriod?> GetManifestFormPeriodByIdAsync(int manifestFormPeriodId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<ManifestFormPeriod> query = dbContext.ManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == manifestFormPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<ManifestFormPeriod>?> GetManifestFormPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<ManifestFormPeriod> query = dbContext.ManifestFormPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteManifestFormPeriodAsync(int manifestFormPeriodId, CancellationToken cancellationToken)
    {
        var manifestFormPeriod = await dbContext.ManifestFormPeriods.SingleOrDefaultAsync(item => item.Id == manifestFormPeriodId, cancellationToken);
        if (manifestFormPeriod == null)
            return;

        dbContext.ManifestFormPeriods.Remove(manifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<ManifestFormPeriod>, int)> GetAllManifestFormPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.ManifestFormPeriods.AsNoTracking()
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

        var columnsSelector = new Dictionary<string, Expression<Func<ManifestFormPeriod, object>>>
        {
            { nameof(ManifestFormPeriod.Code), item => item.Code},
            { nameof(ManifestFormPeriod.Active), item => item.Active},
            { nameof(ManifestFormPeriod.StartNumber), item => item.StartNumber}
        };

        sortBy ??= nameof(ManifestFormPeriod.Code);

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

    public async Task<ManifestFormPeriod?> GetManifestFormPeriodByCodeAsync(string manifestFormPeriodCode, int companyId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<ManifestFormPeriod> query = dbContext.ManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == manifestFormPeriodCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
