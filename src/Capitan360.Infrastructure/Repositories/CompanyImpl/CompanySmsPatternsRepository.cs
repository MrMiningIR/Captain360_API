using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanySmsPatternsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanySmsPatternsRepository
{
    public async Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken)
    {
        dbContext.CompanySmsPatterns.Add(companySmsPatterns);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companySmsPatterns.Id;
    }

    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int companySmsPatternsId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanySmsPatterns> query = dbContext.CompanySmsPatterns;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.SingleOrDefaultAsync(item =>item.Id == companySmsPatternsId, cancellationToken);
    }

    public async Task DeleteCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetAllCompanySmsPatternsAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanySmsPatterns.AsNoTracking()
                                                    .Where(item => searchPhraseLower == null || item.Company.Name.ToLower().Contains(searchPhraseLower) ||
                                                                                                item.SmsPanelNumber.ToLower().Contains(searchPhraseLower) ||
                                                                                                item.SmsPanelUserName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item =>item.Company);

        if (CompanyTypeId != 0)
            baseQuery = baseQuery.Where(item =>item.Company.CompanyTypeId == CompanyTypeId);

        if (CompanyId != 0)
            baseQuery = baseQuery.Where(item =>item.CompanyId == CompanyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanySmsPatterns, object>>>
            {
                { nameof(CompanySmsPatterns.Company.Name), item => item.Company.Name }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var companySmsPatterns = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companySmsPatterns, totalCount);
    }

    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanySmsPatterns> query = dbContext.CompanySmsPatterns;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.SingleOrDefaultAsync(item =>item.CompanyId == companyId, cancellationToken);
    }
}