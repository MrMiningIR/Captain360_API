using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces.Repositories.Companies;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyTypeRepository
{
    public async Task<bool> CheckExistCompanyTypeNameAsync(string companyTypeName, int? currentCompanyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyTypes.AnyAsync(item => item.TypeName.ToLower() == companyTypeName.ToLower().Trim() && (currentCompanyTypeId == null || item.Id != currentCompanyTypeId), cancellationToken);
    }

    public async Task<int> CreateCompanyTypeAsync(CompanyType companyType, CancellationToken cancellationToken)
    {
        dbContext.CompanyTypes.Add(companyType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyType.Id;
    }

    public async Task<CompanyType?> GetCompanyTypeByIdAsync(int companyTypeId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyType> query = dbContext.CompanyTypes;

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyTypeId, cancellationToken);
    }

    public async Task DeleteCompanyTypeAsync(int companyTypeId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyType>, int)> GetAllCompanyTypesAsync(string? searchPhrase, string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyTypes.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.DisplayName.ToLower().Contains(searchPhraseLower) ||
                                                                                        item.TypeName.ToLower().Contains(searchPhraseLower) ||
                                                                                        item.Description.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyType, object>>>
        {
            { nameof(CompanyType.TypeName), item => item.TypeName},
            { nameof(CompanyType.DisplayName), item => item.DisplayName}
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