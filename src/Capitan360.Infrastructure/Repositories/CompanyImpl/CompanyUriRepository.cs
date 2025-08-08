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

        dbContext.CompanyUris.Add(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyUri.Id;
    }
    public async Task<bool> CheckExistUriAsync(string uri, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyUris.AnyAsync(a => a.Uri.ToLower().Trim() == uri.ToLower().Trim(),
            cancellationToken: cancellationToken);
    }
    public void Delete(CompanyUri companyUri)
    {
        dbContext.Entry(companyUri).Property("Deleted").CurrentValue = true;

    }

    public async Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.CompanyUris.SingleOrDefaultAsync(a => a.Id == companyUriId, cancellationToken) :
                          await dbContext.CompanyUris.AsNoTracking().SingleOrDefaultAsync(a => a.Id == companyUriId, cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string? searchPhrase, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
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


}