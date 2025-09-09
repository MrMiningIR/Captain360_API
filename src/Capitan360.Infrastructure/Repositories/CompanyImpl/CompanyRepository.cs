using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyRepository
{
    public async Task<List<int>> GetCompaniesIdByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
                                        .Where(x => x.CompanyTypeId == companyTypeId)
                                        .Select(x => x.Id)
                                        .ToListAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyNameAsync(string companyName, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(cm => (currentCompanyId == null || cm.Id != currentCompanyId) && cm.Name.ToLower() == companyName.ToLower().Trim(), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyCodeAsync(string companyCode, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(cm => (currentCompanyId == null || cm.Id != currentCompanyId) && cm.Code.ToLower() == companyCode.ToLower().Trim(), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyIsParentCompanyAsync(int companyTypeId, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(cm => (currentCompanyId == null || cm.Id != currentCompanyId) && cm.IsParentCompany == true && cm.CompanyTypeId == companyTypeId, cancellationToken);
    }

    public async Task<int> CreateCompanyAsync(Company companyEntity, CancellationToken cancellationToken)
    {
        dbContext.Companies.Add(companyEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyEntity.Id;
    }

    public async Task<Company?> GetCompanyByIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<Company> query = dbContext.Companies;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.CompanyType);

        return await query.SingleOrDefaultAsync(a => a.Id == companyId, cancellationToken);
    }

    public async Task DeleteCompanyAsync(Company company)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<Company>, int)> GetMatchingAllCompaniesAsync(string? searchPhrase, string? sortBy, int CompanyId, int companyTypeId, int cityId, int isParentCompany, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Companies.AsNoTracking()
                                            .Where(com => searchPhraseLower == null || com.Name.ToLower().Contains(searchPhraseLower) ||
                                                                                       com.Code.ToLower().Contains(searchPhraseLower));
        if (loadData)
            baseQuery = baseQuery.Include(a => a.CompanyType);

        if (companyTypeId > 1)
            baseQuery = baseQuery.Where(c => c.CompanyTypeId == companyTypeId);

        if (CompanyId != 0)
            baseQuery = baseQuery.Where(c => c.Id == CompanyId);

        if (cityId != 0)
            baseQuery = baseQuery.Where(c => c.CityId == cityId);

        baseQuery = isParentCompany switch
        {
            1 => baseQuery.Where(a => a.IsParentCompany),
            0 => baseQuery.Where(a => !a.IsParentCompany),
            _ => baseQuery
        };

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Company, object>>>
            {
                { nameof(Company.Name), r => r.Name },
                { nameof(Company.Code), r => r.Code },
                { nameof(Company.Active), r => r.Active },
            };

        sortBy ??= nameof(Company.Name);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companies = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companies, totalCount);
    }




    public async Task<IReadOnlyList<Company>> GetAllCompaniesAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
            .Where(x => companyTypeId == 0 || x.CompanyTypeId == companyTypeId)

            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ValidateCompanyDataWithUserCompanyTypeAsync(int userCompanyType, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
            .AnyAsync(x => x.Id == companyId && x.CompanyTypeId == userCompanyType, cancellationToken: cancellationToken);
    }
}