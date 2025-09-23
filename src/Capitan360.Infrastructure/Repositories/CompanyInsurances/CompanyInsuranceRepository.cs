using Capitan360.Domain.Interfaces;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Capitan360.Domain.Enums;
using NetTopologySuite.Index.HPRtree;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;

namespace Capitan360.Infrastructure.Repositories.CompanyInsurances;

public class CompanyInsuranceRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyInsuranceRepository
{
    public async Task<bool> CheckExistCompanyInsuranceNameAsync(string companyInsuranceName, int companyId, int? currentCompanyInsuranceId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsurances.AnyAsync(item => item.Name.ToLower() == companyInsuranceName.ToLower().Trim() && item.CompanyId == companyId && (currentCompanyInsuranceId == null || item.Id != currentCompanyInsuranceId), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyInsuranceCodeAsync(string companyInsuranceCode, int companyId, int? currentCompanyInsuranceId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsurances.AnyAsync(item => item.Code.ToLower() == companyInsuranceCode.ToLower().Trim() && item.CompanyId == companyId && (currentCompanyInsuranceId == null || item.Id != currentCompanyInsuranceId), cancellationToken);
    }

    public async Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsurances.Add(companyInsurance);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsurance.Id;
    }

    public async Task<CompanyInsurance?> GetCompanyInsuranceByIdAsync(int companyInsuranceId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyInsurance> query = dbContext.CompanyInsurances;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyInsuranceId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyInsurance>?> GetCompanyInsuranceByCompanyIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyInsurance> query = dbContext.CompanyInsurances;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyInsuranceAsync(int companyInsuranceId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyInsurance>, int)> GetAllCompanyInsurancesAsync(string? searchPhrase, string? sortBy, int companyId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyInsurances.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.Code.ToLower().Contains(searchPhraseLower) || item.Name.ToLower().Contains(searchPhraseLower) || item.Description.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company);

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        if (companyId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyInsurance, object>>>
        {
            { nameof(CompanyInsurance.Code), item => item.Code},
            { nameof(CompanyInsurance.Name), item => item.Name},
            { nameof(CompanyInsurance.Active), item => item.Active},
            { nameof(CompanyInsurance.CaptainCargoCode), item => item.CaptainCargoCode}
        };

        sortBy ??= nameof(CompanyInsurance.Code);

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

    public async Task<CompanyInsurance?> GetCompanyInsuranceByCodeAsync(string companyInsuranceCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyInsurance> query = dbContext.CompanyInsurances;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyInsuranceCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
