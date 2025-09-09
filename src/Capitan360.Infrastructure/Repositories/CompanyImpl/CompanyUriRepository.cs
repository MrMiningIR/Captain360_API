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
    public async Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken) //ch**
    {
        dbContext.CompanyUris.Add(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyUri.Id;
    }

    public async Task<bool> CheckExistCompanyUriUriAsync(string companyUriUri, int? currentCompanyUriId, CancellationToken cancellationToken) //ch**
    {
        return await dbContext.CompanyUris.AnyAsync(cu => (currentCompanyUriId == null || cu.Id != currentCompanyUriId) && cu.Uri.ToLower() == companyUriUri.ToLower().Trim(), cancellationToken);
    }

    public async Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool tracked, bool loadData, CancellationToken cancellationToken) //ch**
    {
        IQueryable<CompanyUri> query = dbContext.CompanyUris;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyUriId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyUri>?> GetCompanyUriByCompanyIdAsync(int companyUriCompanyId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanyUri> query = dbContext.CompanyUris;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.Where(a => a.CompanyId == companyUriCompanyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string? searchPhrase, string? sortBy, int companyId, int active, int captain360Uri, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken) //ch**
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyUris.AsNoTracking()
                                            .Where(cu => searchPhraseLower == null || cu.Description.ToLower().Contains(searchPhraseLower) || cu.Uri.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        baseQuery = captain360Uri switch
        {
            1 => baseQuery.Where(a => a.Captain360Uri),
            0 => baseQuery.Where(a => !a.Captain360Uri),
            _ => baseQuery
        };

        if (companyId > 1)
            baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyUri, object>>>
        {
            { nameof(CompanyUri.Uri), pt => pt.Uri}
        };
        sortBy ??= nameof(CompanyUri.Uri);
        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyUris = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyUris, totalCount);
    }

    public async Task DeleteCompanyUriAsync(CompanyUri companyUri)
    {
        await Task.Yield();
    }
}