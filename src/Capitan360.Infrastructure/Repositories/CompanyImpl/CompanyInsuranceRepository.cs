using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyInsuranceRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyInsuranceRepository
{
    public async Task<bool> CheckExistCompanyInsuranceNameAsync(string companyInsuranceName, int? currentCompanyInsuranceId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsurances.AnyAsync(ci => ci.CompanyId == companyId && (currentCompanyInsuranceId == null || ci.Id != currentCompanyInsuranceId) && ci.Name.ToLower() == companyInsuranceName.ToLower().Trim(), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyInsuranceCodeAsync(string companyInsuranceCode, int? currentCompanyInsuranceId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsurances.AnyAsync(ci => ci.CompanyId == companyId && (currentCompanyInsuranceId == null || ci.Id != currentCompanyInsuranceId) && ci.Code.ToLower() == companyInsuranceCode.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsurances.Add(companyInsurance);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsurance.Id;
    }

    public async Task<CompanyInsurance?> GetCompanyInsuranceByIdAsync(int companyInsuranceId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyInsurance> query = dbContext.CompanyInsurances;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyInsuranceId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyInsurance>?> GetCompanyInsuranceByCompanyIdAsync(int companyInsuranceCompanyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyInsurance> query = dbContext.CompanyInsurances;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.Where(a => a.CompanyId == companyInsuranceCompanyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyInsuranceAsync(CompanyInsurance companyInsurance)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyInsurance>, int)> GetMatchingAllCompanyInsurancesAsync(string? searchPhrase, string? sortBy, int companyId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyInsurances.AsNoTracking()
                                              .Where(cu => searchPhraseLower == null || cu.Code.ToLower().Contains(searchPhraseLower) || cu.Name.ToLower().Contains(searchPhraseLower) || cu.Description.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        if (companyId != 0)
            baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyInsurance, object>>>
        {
            { nameof(CompanyInsurance.Name), pt => pt.Name},
            { nameof(CompanyInsurance.Code), pt => pt.Code},
            { nameof(CompanyInsurance.Active), pt => pt.Active},
            { nameof(CompanyInsurance.CaptainCargoCode), pt => pt.CaptainCargoCode}
        };

        sortBy ??= nameof(CompanyInsurance.Name);

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
