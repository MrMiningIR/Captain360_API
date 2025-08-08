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

    public void Delete(CompanySmsPatterns companySmsPatterns, string userId)
    {
        dbContext.Entry(companySmsPatterns).Property("Deleted").CurrentValue = true;
    }

    public async Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatternsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.CompanySmsPatterns.ToListAsync(cancellationToken);
    }

    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int companySmsPatternsId, bool tracked,
        CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.CompanySmsPatterns.SingleOrDefaultAsync(a => a.Id == companySmsPatternsId, cancellationToken) :
                 await dbContext.CompanySmsPatterns.AsNoTracking().SingleOrDefaultAsync(a => a.Id == companySmsPatternsId, cancellationToken);
    }
    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.CompanySmsPatterns.SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken) :
                         await dbContext.CompanySmsPatterns.AsNoTracking().SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken);
    }
    public async Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetAllCompanySmsPatterns(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanySmsPatterns
            .Where(csp => searchPhraseLower == null || csp.PatternSmsIssueSender.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanySmsPatterns, object>>>
            {
                { nameof(CompanySmsPatterns.PatternSmsIssueSender), csp => csp.PatternSmsIssueSender },
                { nameof(CompanySmsPatterns.CompanyId), csp => csp.CompanyId }
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
}