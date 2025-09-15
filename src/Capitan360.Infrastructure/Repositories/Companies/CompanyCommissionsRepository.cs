using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyCommissionsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyCommissionsRepository
{
    public async Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, CancellationToken cancellationToken)
    {
        dbContext.CompanyCommissions.Add(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyCommissions.Id;
    }

    public async Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int companyCommissionsId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyCommissions> query = dbContext.CompanyCommissions;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item =>item.Id == companyCommissionsId, cancellationToken);
    }

    public async Task DeleteCompanyCommissionsAsync(int companyCommissionsId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyCommissions>, int)> GetAllCompanyCommissionsAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyCommissions.AsNoTracking()
                                                    .Where(item => searchPhraseLower == null || item.Company!.Name.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item =>item.Company);

        if (CompanyTypeId != 0)
            baseQuery = baseQuery.Where(item =>item.Company!.CompanyTypeId == CompanyTypeId);

        if (CompanyId != 0)
            baseQuery = baseQuery.Where(item =>item.CompanyId == CompanyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyCommissions, object>>>
            {
                { nameof(CompanyCommissions.Company.Name), item => item.Company!.Name }
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

    public async Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyCommissions> query = dbContext.CompanyCommissions;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item =>item.CompanyId == companyId, cancellationToken);
    }
}