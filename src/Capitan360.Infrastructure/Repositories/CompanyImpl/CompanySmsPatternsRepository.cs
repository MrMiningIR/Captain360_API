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
    public async Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken) //ch**
    {
        dbContext.CompanySmsPatterns.Add(companySmsPatterns);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companySmsPatterns.Id;
    }

    public async Task DeleteCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, string userId) //ch**
    {
        await Task.Yield();
    }

    public async Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatternsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.CompanySmsPatterns.ToListAsync(cancellationToken);
    }

    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int companySmsPatternsId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanySmsPatterns> query = dbContext.CompanySmsPatterns;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companySmsPatternsId, cancellationToken);
    }
    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanySmsPatterns> query = dbContext.CompanySmsPatterns;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken);
    }
    public async Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetMatchingAllCompanySmsPatternsAsync
        (string? searchPhrase, string? sortBy, int companyTypeId, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)//ch**
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanySmsPatterns.AsNoTracking()
                                                    .Where(cc => searchPhraseLower == null || cc.Company.Name.ToLower().Contains(searchPhraseLower) ||
                                                                                              cc.SmsPanelNumber.ToLower().Contains(searchPhraseLower) ||
                                                                                              cc.SmsPanelUserName.ToLower().Contains(searchPhraseLower));


        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        if (companyTypeId > 1)
            baseQuery = baseQuery.Where(a => a.Company.CompanyTypeId == companyTypeId);

        if (companyId != 0)
            baseQuery = baseQuery.Where(a => a.CompanyId == companyId);


        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanySmsPatterns, object>>>
            {
                { nameof(CompanySmsPatterns.PatternSmsIssueSender), csp => csp.PatternSmsIssueSender },
                { nameof(CompanySmsPatterns.CompanyId), csp => csp.CompanyId }
            };
        if (sortBy == null)
        {
            sortBy ??= nameof(CompanySmsPatterns.CompanyId);
        }
        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companySmsPatterns = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companySmsPatterns, totalCount);
    }
}