using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyTypeRepository
{

    public async Task<bool> CheckExistCompanyTypeNameAsync(string companyTypeName, int? currentCompanyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyTypes.AnyAsync(ct => (currentCompanyTypeId == null || ct.Id != currentCompanyTypeId) && ct.TypeName.ToLower() == companyTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> CreateCompanyTypeAsync(CompanyType companyType, CancellationToken cancellationToken)
    {
        dbContext.CompanyTypes.Add(companyType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyType.Id;
    }

    public async Task<CompanyType?> GetCompanyTypeByIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyType> query = dbContext.CompanyTypes;

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(a => a.Id == companyTypeId, cancellationToken);
    }

    public async Task DeleteCompanyTypeAsync(CompanyType companyType)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyType>, int)> GetMatchingAllCompanyTypesAsync(string? searchPhrase, string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyTypes.AsNoTracking()
                                              .Where(ct => searchPhraseLower == null || ct.DisplayName.ToLower().Contains(searchPhraseLower) ||
                                                                                        ct.TypeName.ToLower().Contains(searchPhraseLower) ||
                                                                                        ct.Description.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyType, object>>>
        {
            { nameof(CompanyType.TypeName), pt => pt.TypeName},
            { nameof(CompanyType.DisplayName), pt => pt.DisplayName}
        };

        sortBy ??= nameof(CompanyType.TypeName);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var packageTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (packageTypes, totalCount);
    }
}