using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyRepository
{
    public async Task<List<int>> GetCompaniesIdByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
                                        .Where(item => item.CompanyTypeId == companyTypeId)
                                        .Select(item => item.Id)
                                        .ToListAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyNameAsync(string companyName, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(item => item.Name.ToLower() == companyName.Trim().ToLower() && (currentCompanyId == null || item.Id != currentCompanyId), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyCodeAsync(string companyCode, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(item => item.Code.ToLower() == companyCode.Trim().ToLower() && (currentCompanyId == null || item.Id != currentCompanyId), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyIsParentAsync(int companyTypeId, int? currentCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AnyAsync(item => item.IsParentCompany == true && item.CompanyTypeId == companyTypeId && (currentCompanyId == null || item.Id != currentCompanyId), cancellationToken);
    }

    public async Task<int> CreateCompanyAsync(Company companyEntity, CancellationToken cancellationToken)
    {
        dbContext.Companies.Add(companyEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyEntity.Id;
    }

    public async Task<Company?> GetCompanyByIdAsync(int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<Company> query = dbContext.Companies;

        if (loadData)
            query = query.Include(item => item.CompanyType);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyId, cancellationToken);
    }

    public async Task DeleteCompanyAsync(int companyId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<Company>, int)> GetAllCompaniesAsync(string searchPhrase, string? sortBy, int CompanyId, int companyTypeId, int cityId, int isParentCompany, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();

        var baseQuery = dbContext.Companies.AsNoTracking()
                                            .Where(item => item.Name.ToLower().Contains(searchPhrase) || item.MobileCounter.ToLower().Contains(searchPhrase) ||
                                                           item.Code.ToLower().Contains(searchPhrase));
        if (loadData || true)
            baseQuery = baseQuery.Include(item => item.CompanyType).Include(x => x.CompanyPreferences);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);

        if (CompanyId != 0)
            baseQuery = baseQuery.Where(item => item.Id == CompanyId);

        if (cityId != 0)
            baseQuery = baseQuery.Where(item => item.CityId == cityId);

        baseQuery = isParentCompany switch
        {
            1 => baseQuery.Where(item => item.IsParentCompany),
            0 => baseQuery.Where(item => !item.IsParentCompany),
            _ => baseQuery
        };

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Company, object>>>
            {
                { nameof(Company.Code), item =>  item.Code },
                { nameof(Company.Name), item =>  item.Name },
                { nameof(Company.CompanyType.TypeName), item =>  item.CompanyType!.TypeName},
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

    public async Task<Company?> GetCompanyByCodeAsync(string companyCode, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<Company> query = dbContext.Companies;

        if (loadData)
            query = query.Include(item => item.CompanyType);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyCode.Trim().ToLower(), cancellationToken);
    }

    public async Task<IReadOnlyList<Company>> GetAllCompaniesAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
            .Where(item => companyTypeId == 0 || item.CompanyTypeId == companyTypeId)

            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ValidateCompanyDataWithUserCompanyTypeAsync(int userCompanyType, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
            .AnyAsync(item => item.Id == companyId && item.CompanyTypeId == userCompanyType, cancellationToken);
    }
}