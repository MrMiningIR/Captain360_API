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
    public async Task<int> CreateCompanyAsync(Company companyEntity, CancellationToken cancellationToken)
    {
        dbContext.Companies.Add(companyEntity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyEntity.Id;
    }

    public void Delete(Company company, string userId)
    {
        dbContext.Entry(company).Property("Deleted").CurrentValue = true;
    }

    public async Task<IReadOnlyList<Company>> GetAllCompanies(CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()

            .ToListAsync(cancellationToken);
    }

    public async Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken, int userCompanyTypeId = 0, bool track = false)
    {
        var dbQuery = dbContext.Companies.Where(x => x.Id == id);
        dbQuery = !track ? dbQuery.AsNoTracking() : dbQuery;
        if (userCompanyTypeId > 0)
        {
            dbQuery = dbQuery.Where(x => x.CompanyTypeId == userCompanyTypeId);
        }

        return await dbQuery.SingleOrDefaultAsync(cancellationToken);

        //.Include(c => c.CompanyType)
        //.Include(c => c.CompanyAddresses)
        //.ThenInclude(c => c.Address).ThenInclude(c => c.Country)
        //.Include(c => c.CompanyAddresses).ThenInclude(c => c.Address).ThenInclude(c => c.City)
        //.Include(c => c.CompanyAddresses).ThenInclude(c => c.Address).ThenInclude(c => c.Province)
        //.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<Company>, int)> GetMatchingAllCompanies(string? searchPhrase, int companyTypeId,
        int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Companies
            .Include(c => c.CompanyType)
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                      || r.Code.ToLower().Contains(searchPhraseLower)));
        if (companyTypeId > 0)
        {
            baseQuery = baseQuery.Where(c => c.CompanyTypeId == companyTypeId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Company, object>>>
            {
                { nameof(Company.Name), r => r.Name },
                { nameof(Company.CompanyTypeId), r => r.CompanyTypeId },
                { nameof(Company.Active), r => r.Active },
            };

        Expression<Func<Company, object>> selectedColumn;
        selectedColumn = sortBy != null ? columnsSelector[sortBy] : columnsSelector[nameof(Company.Name)];

        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companies = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return (companies, totalCount);
    }

    //--
    public async Task<List<int>> GetCompaniesIdByCompanyTypeId(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking().Where(x => x.CompanyTypeId == companyTypeId)
            .Select(x => x.Id).ToListAsync(cancellationToken);
    }

    public async Task<bool> ValidateCompanyDataWithUserCompanyType(int userCompanyType, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.Companies.AsNoTracking()
            .AnyAsync(x => x.Id == companyId && x.CompanyTypeId == userCompanyType, cancellationToken: cancellationToken);
    }
}