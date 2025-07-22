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

    public async Task<int> CreateCompanyTypeAsync(CompanyType companyType, string userId, CancellationToken cancellationToken)
    {
        dbContext.Entry(companyType).Property("CreatedBy").CurrentValue = userId;
        dbContext.CompanyTypes.Add(companyType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyType.Id;
    }

    public void Delete(CompanyType companyType, string userId)
    {
        dbContext.Entry(companyType).Property("Deleted").CurrentValue = true;
        UpdateShadows(companyType, userId);
    }

    public async Task<IReadOnlyList<CompanyType>> GetAllCompanyTypes(CancellationToken cancellationToken)
    {
        return await dbContext.CompanyTypes.ToListAsync(cancellationToken);
    }

    public async Task<CompanyType?> GetCompanyTypeById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyTypes.FirstOrDefaultAsync(ct => ct.Id == id, cancellationToken);
    }

    public CompanyType UpdateShadows(CompanyType companyType, string userId)
    {
        dbContext.Entry(companyType).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        dbContext.Entry(companyType).Property("UpdatedBy").CurrentValue = userId;
        return companyType;
    }

    public async Task<(IReadOnlyList<CompanyType>, int)> GetMatchingAllCompanyTypes(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyTypes
            .Where(ct => searchPhraseLower == null ||
                        ct.TypeName.ToLower().Contains(searchPhraseLower) ||
                        ct.DisplayName.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyType, object>>>
            {
                { nameof(CompanyType.TypeName), ct => ct.TypeName },
                { nameof(CompanyType.DisplayName), ct => ct.DisplayName },
                { nameof(CompanyType.Id), ct => ct.Id }
            };

        Expression<Func<CompanyType, object>> selectedColumn;
        selectedColumn = sortBy != null ? columnsSelector[sortBy] : columnsSelector[nameof(CompanyType.Id)];
        baseQuery = sortDirection == SortDirection.Ascending
    ? baseQuery.OrderBy(selectedColumn)
    : baseQuery.OrderByDescending(selectedColumn);

        var companyTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyTypes, totalCount);
    }
}