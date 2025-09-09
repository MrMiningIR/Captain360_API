using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyCommissionsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyCommissionsRepository
{
    public async Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions,//ch**
        CancellationToken cancellationToken)
    {
        dbContext.CompanyCommissions.Add(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyCommissions.Id;
    }

    public async Task DeleteCompanyCommissionsAsync(CompanyCommissions companyCommissions) //ch**
    {
        await Task.Yield();

    }



    public async Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int companyCommissionsId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanyCommissions> query = dbContext.CompanyCommissions;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyCommissionsId, cancellationToken);
    }
    public async Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanyCommissions> query = dbContext.CompanyCommissions;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.CompanyId == companyId, cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissionsAsync(string? searchPhrase, string? sortBy, int companyTypeId, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)//ch**
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyCommissions.AsNoTracking()
                                                    .Where(cc => searchPhraseLower == null || cc.Company!.Name.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        if (companyTypeId > 1)
            baseQuery = baseQuery.Where(a => a.Company!.CompanyTypeId == companyTypeId);

        if (companyId != 0)
            baseQuery = baseQuery.Where(a => a.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);


        var columnsSelector = new Dictionary<string, Expression<Func<CompanyCommissions, object>>>
            {
                { nameof(CompanyCommissions.Company.Name), cc => cc.Company !.Name }
            };

        sortBy ??= nameof(CompanyCommissions.Company.Name);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyCommissions = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyCommissions, totalCount);
    }
}