using System.Linq.Expressions;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanySmsPatternsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanySmsPatternsRepository
{
    public async Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, string userId, CancellationToken cancellationToken)
    {
        dbContext.Entry(companySmsPatterns).Property("CreatedBy").CurrentValue = userId;
        dbContext.CompanySmsPatterns.Add(companySmsPatterns);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companySmsPatterns.Id;
    }

    public void Delete(CompanySmsPatterns companySmsPatterns, string userId)
    {
        dbContext.Entry(companySmsPatterns).Property("Deleted").CurrentValue = true;
        UpdateShadows(companySmsPatterns, userId);
    }

    public async Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatterns(CancellationToken cancellationToken)
    {
        return await dbContext.CompanySmsPatterns.ToListAsync(cancellationToken);
    }

    public async Task<CompanySmsPatterns?> GetCompanySmsPatternsById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanySmsPatterns.FirstOrDefaultAsync(csp => csp.Id == id, cancellationToken);
    }

    public CompanySmsPatterns UpdateShadows(CompanySmsPatterns companySmsPatterns, string userId)
    {
        dbContext.Entry(companySmsPatterns).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        dbContext.Entry(companySmsPatterns).Property("UpdatedBy").CurrentValue = userId;
        return companySmsPatterns;
    }

    public async Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetMatchingAllCompanySmsPatterns(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
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