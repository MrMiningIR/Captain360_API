using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyPreferencesRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyPreferencesRepository
{
    public async Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken)
    {
        dbContext.CompanyPreferences.Add(companyPreferences);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyPreferences.Id;
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int companyPreferencesId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPreferences> query = dbContext.CompanyPreferences;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyPreferencesId, cancellationToken);
    }

    public async Task DeleteCompanyPreferencesAsync(CompanyPreferences companyPreferences)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyPreferences>, int)> GetMatchingAllCompanyPreferencesAsync(string? searchPhrase, string? sortBy, int companyTypeId, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyPreferences.AsNoTracking()
                                                    .Where(cc => searchPhraseLower == null || cc.Company.Name.ToLower().Contains(searchPhraseLower) ||
                                                                                              cc.CaptainCargoCode.ToLower().Contains(searchPhraseLower) ||
                                                                                              cc.CaptainCargoName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(a => a.Company.CompanyTypeId == companyTypeId);

        if (companyId != 0)
            baseQuery = baseQuery.Where(a => a.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPreferences, object>>>
            {
                { nameof(CompanyPreferences.Company.Name), cc => cc.Company.Name }
            };
        sortBy ??= nameof(CompanyPreferences.Company.Name);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyPreferences = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyPreferences, totalCount);
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPreferences> query = dbContext.CompanyPreferences;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken);
    }
}