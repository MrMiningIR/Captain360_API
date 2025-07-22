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
    public async Task<int> CreateCompanyInsuranceAsync(CompanyInsurance companyInsurance, CancellationToken cancellationToken)
    {
        dbContext.CompanyInsurances.Add(companyInsurance);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyInsurance.Id;
    }

    public void Delete(CompanyInsurance companyInsurance)
    {
        dbContext.Entry(companyInsurance).Property("Deleted").CurrentValue = true;
    }

    public async Task<CompanyInsurance?> GetCompanyInsuranceById(int id, CancellationToken cancellationToken, bool track = false)
    {
        if (track)
        {

            return await dbContext.CompanyInsurances
                .Include(a => a.CompanyType)
                //  .Include(a => a.Company)
                // .Include(a=>a.CompanyInsuranceCharges)
                .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
            // .Include(a => a.CompanyInsuranceCharge)
        }
        else
        {
            return await dbContext.CompanyInsurances.AsNoTracking()
            .Include(a => a.CompanyType)
            //  .Include(a => a.Company)
            // .Include(a=>a.CompanyInsuranceCharges)
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
            // .Include(a => a.CompanyInsuranceCharge)
        }
    }

    public async Task<(IReadOnlyList<CompanyInsurance>, int)> GetMatchingAllCompanyInsurances(string? searchPhrase, int companyTypeId, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyInsurances
            .Include(a => a.CompanyType)
            .Include(a => a.Company)
            .AsNoTracking()
            .Where(ci => string.IsNullOrEmpty(searchPhraseLower)
                         || ci.Name.ToLower().Contains(searchPhraseLower)
                         || ci.Code.ToLower().Contains(searchPhraseLower));

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(ci => ci.CompanyTypeId == companyTypeId);

        if (companyId != 0)
            baseQuery = baseQuery.Where(ci => ci.CompanyId == companyId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            2 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyInsurance, object>>>
        {
            { nameof(CompanyInsurance.Name), ci => ci.Name },
            { nameof(CompanyInsurance.Tax), ci => ci.Tax },
            { nameof(CompanyInsurance.Scale), ci => ci.Scale },
        };

        sortBy ??= nameof(CompanyInsurance.Name);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyInsurances = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyInsurances, totalCount);
    }

    public async Task<bool> CheckExistCompanyInsuranceName(string name, int companyTypeId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyInsurances
            .AnyAsync(ci => ci.CompanyTypeId == companyTypeId && ci.CompanyId == companyId && ci.Name.ToLower().Trim() == name.ToLower().Trim(), cancellationToken);
    }
}
