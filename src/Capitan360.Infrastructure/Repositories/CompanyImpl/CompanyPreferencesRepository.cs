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

    public async Task<IReadOnlyList<CompanyPreferences>> GetAllCompanyPreferences(CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPreferences.ToListAsync(cancellationToken);
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByCompanyId(int id, CancellationToken cancellationToken, bool track = false)
    {
        if (track)
        {
            return await dbContext.CompanyPreferences.SingleOrDefaultAsync(cp => cp.CompanyId == id, cancellationToken);
        }
        else
        {
            return await dbContext.CompanyPreferences.AsNoTracking().SingleOrDefaultAsync(cp => cp.CompanyId == id, cancellationToken);
        }
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesById(int id, CancellationToken cancellationToken, bool track = false)
    {
        if (track)
        {
            return await dbContext.CompanyPreferences.SingleOrDefaultAsync(cp => cp.Id == id, cancellationToken);
        }
        else
        {
            return await dbContext.CompanyPreferences.AsNoTracking().SingleOrDefaultAsync(cp => cp.Id == id, cancellationToken);
        }
    }



    public async Task<(IReadOnlyList<CompanyPreferences>, int)> GetMatchingAllCompanyPreferences(string? searchPhrase,
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