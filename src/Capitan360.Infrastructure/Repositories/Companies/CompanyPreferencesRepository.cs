using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Capitan360.Domain.Enums;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyPreferencesRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyPreferencesRepository
{
    public async Task<int> CreateCompanyPreferencesAsync(CompanyPreferences companyPreferences, CancellationToken cancellationToken)
    {
        dbContext.CompanyPreferences.Add(companyPreferences);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyPreferences.Id;
    }

    public async Task<CompanyPreferences?> GetCompanyPreferencesByIdAsync(int companyPreferencesId, bool loadData, bool tracked,CancellationToken cancellationToken)
    {
        IQueryable<CompanyPreferences> query = dbContext.CompanyPreferences;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item =>item.Id == companyPreferencesId, cancellationToken);
    }

    public async Task DeleteCompanyPreferencesAsync(int companyPreferencesId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyPreferences>, int)> GetAllCompanyPreferencesAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyPreferences.AsNoTracking()
                                                    .Where(item => searchPhraseLower == null || item.Company!.Name.ToLower().Contains(searchPhraseLower) ||
                                                                                                item.CaptainCargoCode.ToLower().Contains(searchPhraseLower) ||
                                                                                                item.CaptainCargoName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item =>item.Company);

        if (CompanyTypeId != 0)
            baseQuery = baseQuery.Where(item =>item.Company!.CompanyTypeId == CompanyTypeId);

        if (CompanyId != 0)
            baseQuery = baseQuery.Where(item =>item.CompanyId == CompanyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyPreferences, object>>>
            {
                { nameof(CompanyPreferences.Company.Name), item => item.Company!.Name }
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

    public async Task<CompanyPreferences?> GetCompanyPreferencesByCompanyIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPreferences> query = dbContext.CompanyPreferences;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item =>item.CompanyId == companyId, cancellationToken);
    }
}