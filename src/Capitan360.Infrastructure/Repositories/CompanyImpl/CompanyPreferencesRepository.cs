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
    public async Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences,
        CancellationToken cancellationToken)
    {
        dbContext.CompanyPreferences.Add(companyPreferences);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyPreferences.Id;
    }

    public void Delete(CompanyPreferences companyPreferences, string userId)
    {
        dbContext.Entry(companyPreferences).Property("Deleted").CurrentValue = true;
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int companyId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.CompanyPreferences.SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken) :
                          await dbContext.CompanyPreferences.AsNoTracking().SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int companyPreferencesId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.CompanyPreferences.SingleOrDefaultAsync(a => a.Id == companyPreferencesId, cancellationToken) :
                       await dbContext.CompanyPreferences.AsNoTracking().SingleOrDefaultAsync(a => a.Id == companyPreferencesId, cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyPreferences>, int)> GetAllCompanyPreferencesAsync(string? searchPhrase,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyPreferences
            .Where(cp => searchPhraseLower == null || cp.EconomicCode.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyPreferences, object>>>
            {
                { nameof(CompanyPreferences.EconomicCode), cp => cp.EconomicCode },
                { nameof(CompanyPreferences.CompanyId), cp => cp.CompanyId }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var companyPreferences = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyPreferences, totalCount);
    }
}