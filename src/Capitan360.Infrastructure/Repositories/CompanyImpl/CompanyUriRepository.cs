using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyUriRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyUriRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyUriRepository
{
    public async Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken)
    {
        // dbContext.Entry(companyUri).Property("CreatedBy").CurrentValue = userId;
        dbContext.CompanyUris.Add(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyUri.Id;
    }

    public void Delete(CompanyUri companyUri)
    {
        dbContext.Entry(companyUri).Property("Deleted").CurrentValue = true;
        // UpdateShadows(companyUri, userId);
    }

    public async Task<CompanyUri?> GetCompanyUriById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyUris.FirstOrDefaultAsync(cu => cu.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyUri>, int)> GetMatchingAllCompanyUris(string? searchPhrase, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyUris.AsNoTracking()
            .Where(c => c.CompanyId == companyId)
            .Where(cu => searchPhraseLower == null || cu.Uri.ToLower().Contains(searchPhraseLower));

        switch (active)
        {
            case 1:
                baseQuery = baseQuery.Where(x => x.IsActive);
                break;

            case 0:
                baseQuery = baseQuery.Where(x => !x.IsActive);
                break;
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyUri, object>>>
            {
                { nameof(CompanyUri.Id), cu => cu.Id },
                { nameof(CompanyUri.IsActive), cu => cu.IsActive },
                { nameof(CompanyUri.CompanyId), cu => cu.CompanyId }
            };

        Expression<Func<CompanyUri, object>> selectedColumn;
        if (sortBy == null)
        {
            selectedColumn = columnsSelector[nameof(CompanyUri.Id)];
        }
        else
        {
            selectedColumn = columnsSelector[sortBy];
        }
        baseQuery = sortDirection == SortDirection.Ascending
    ? baseQuery.OrderBy(selectedColumn)
    : baseQuery.OrderByDescending(selectedColumn);

        var companyUris = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyUris, totalCount);
    }

    public async Task<bool> CheckExistUri(string uri, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyUris.AnyAsync(a => a.Uri.ToLower().Trim() == uri.ToLower().Trim(),
            cancellationToken: cancellationToken);
    }
}