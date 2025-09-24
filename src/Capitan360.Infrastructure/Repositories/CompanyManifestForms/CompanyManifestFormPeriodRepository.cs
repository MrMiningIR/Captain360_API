using System.Linq.Expressions;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyManifestForms;

public class CompanyManifestFormPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IManifestFormPeriodRepository
{
    public async Task<bool> CheckExistManifestFormPeriodCodeAsync(string companyManifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestFormPeriods.AnyAsync(item => item.CompanyId == companyId && item.Code.ToLower() == companyManifestFormPeriodCode.ToLower().Trim() && (currentManifestFormPeriodId == null || item.Id != currentManifestFormPeriodId), cancellationToken);
    }

    public async Task<int> CreateManifestFormPeriodAsync(CompanyManifestFormPeriod companyManifestFormPeriod, CancellationToken cancellationToken)
    {
        dbContext.CompanyManifestFormPeriods.Add(companyManifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyManifestFormPeriod.Id;
    }

    public async Task<CompanyManifestFormPeriod?> GetManifestFormPeriodByIdAsync(int companyManifestFormPeriodId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyManifestFormPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyManifestFormPeriod>?> GetManifestFormPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteManifestFormPeriodAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken)
    {
        var companyManifestFormPeriod = await dbContext.CompanyManifestFormPeriods.SingleOrDefaultAsync(item => item.Id == companyManifestFormPeriodId, cancellationToken);
        if (companyManifestFormPeriod == null)
            return;

        dbContext.CompanyManifestFormPeriods.Remove(companyManifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyManifestFormPeriod>, int)> GetAllManifestFormPeriodsAsync(string? searchPhrase, string? sortBy, int companyId, int active, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyManifestFormPeriods.AsNoTracking()
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

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyManifestFormPeriod, object>>>
        {
            { nameof(CompanyManifestFormPeriod.Code), item => item.Code},
            { nameof(CompanyManifestFormPeriod.Active), item => item.Active},
            { nameof(CompanyManifestFormPeriod.StartNumber), item => item.StartNumber}
        };

        sortBy ??= nameof(CompanyManifestFormPeriod.Code);

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

    public async Task<CompanyManifestFormPeriod?> GetManifestFormPeriodByCodeAsync(string companyManifestFormPeriodCode, int companyId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyManifestFormPeriodCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
