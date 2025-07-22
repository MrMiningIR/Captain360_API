using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyCommissionsImpl;

public class CompanyCommissionsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyCommissionsRepository
{
    public async Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, string userId, CancellationToken cancellationToken)
    {
        dbContext.Entry(companyCommissions).Property("CreatedBy").CurrentValue = userId;
        dbContext.CompanyCommissions.Add(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyCommissions.Id;
    }

    public void Delete(CompanyCommissions companyCommissions, string userId)
    {
        dbContext.Entry(companyCommissions).Property("Deleted").CurrentValue = true;
        UpdateShadows(companyCommissions, userId);
    }

    public async Task<IReadOnlyList<CompanyCommissions>> GetAllCompanyCommissions(CancellationToken cancellationToken)
    {
        return await dbContext.CompanyCommissions.ToListAsync(cancellationToken);
    }

    public async Task<CompanyCommissions?> GetCompanyCommissionsById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyCommissions.FirstOrDefaultAsync(cc => cc.Id == id, cancellationToken);
    }

    public CompanyCommissions UpdateShadows(CompanyCommissions companyCommissions, string userId)
    {
        dbContext.Entry(companyCommissions).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        dbContext.Entry(companyCommissions).Property("UpdatedBy").CurrentValue = userId;
        return companyCommissions;
    }

    public async Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissions(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyCommissions.AsQueryable();

        if (!string.IsNullOrEmpty(searchPhrase))
        {
            var searchPhraseLower = searchPhrase.ToLower();
            baseQuery = baseQuery.Where(cc => cc.CommissionFromCaptainCargoWebSite.ToString().Contains(searchPhraseLower));
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyCommissions, object>>>
            {
                { nameof(CompanyCommissions.CommissionFromCaptainCargoWebSite), cc => cc.CommissionFromCaptainCargoWebSite },
                { nameof(CompanyCommissions.CompanyId), cc => cc.CompanyId }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var companyCommissions = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyCommissions, totalCount);
    }
}