using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories;

public class CompanyCommissionsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyCommissionsRepository
{
    public async Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, string userId, CancellationToken cancellationToken)
    {
        dbContext.CompanyCommissions.Add(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyCommissions.Id;
    }

    public void Delete(CompanyCommissions companyCommissions)
    {
        dbContext.Entry(companyCommissions).Property("Deleted").CurrentValue = true;

    }

    public async Task<IReadOnlyList<CompanyCommissions>> GetAllCompanyCommissions(CancellationToken cancellationToken)
    {
        return await dbContext.CompanyCommissions.ToListAsync(cancellationToken);
    }

    public async Task<CompanyCommissions?> GetCompanyCommissionsById(int id, bool tracked,
        CancellationToken cancellationToken)
    {
        if (tracked)
        {

            return await dbContext.CompanyCommissions.SingleOrDefaultAsync(cc => cc.Id == id, cancellationToken);
        }
        else
        {
            return await dbContext.CompanyCommissions.AsNoTracking().SingleOrDefaultAsync(cc => cc.Id == id, cancellationToken);
        }
    }


    public async Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissions(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyCommissions.AsNoTracking();

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