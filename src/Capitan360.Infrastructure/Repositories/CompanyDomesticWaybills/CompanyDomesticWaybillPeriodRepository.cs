using System.Linq.Expressions;
using Capitan360.Domain.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticWaybills;

public class CompanyDomesticWaybillPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticWaybillPeriodRepository
{
    public async Task<bool> CheckExistCompanyDomesticWaybillPeriodCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, int? currentDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybillPeriods.AnyAsync(item => item.Code.ToLower() == companyDomesticWaybillPeriodCode.Trim().ToLower() && item.CompanyId == companyId && (currentDomesticWaybillPeriodId == null || item.Id != currentDomesticWaybillPeriodId), cancellationToken);
    }

    public async Task<int> CreateCompanyDomesticWaybillPeriodAsync(CompanyDomesticWaybillPeriod companyDomesticWaybillPeriod, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticWaybillPeriods.Add(companyDomesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticWaybillPeriod.Id;
    }

    public async Task<CompanyDomesticWaybillPeriod?> GetCompanyDomesticWaybillPeriodByIdAsync(int companyDomesticWaybillPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyDomesticWaybillPeriod>?> GetCompanyDomesticWaybillPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .OrderBy(item => item.Code)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyDomesticWaybillPeriodAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        var companyDomesticWaybillPeriod = await dbContext.CompanyDomesticWaybillPeriods.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillPeriodId, cancellationToken);
        if (companyDomesticWaybillPeriod == null)
            return;

        dbContext.CompanyDomesticWaybillPeriods.Remove(companyDomesticWaybillPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyDomesticWaybillPeriodGetAllDto>, int)> GetAllCompanyDomesticWaybillPeriodsAsync(string searchPhrase, string? sortBy, int companyId, int active, int hasReadyForm, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.CompanyDomesticWaybillPeriods.AsNoTracking()
                                                               .GroupJoin(
                                                                           dbContext.CompanyDomesticWaybills,
                                                                           itemCompanyDomesticWaybillPeriod => itemCompanyDomesticWaybillPeriod.Id,
                                                                           itemCompanyDomesticWaybill => itemCompanyDomesticWaybill.CompanyDomesticWaybillPeriodId,
                                                                           (itemCompanyDomesticWaybillPeriod, itemCompanyDomesticWaybill) => new { itemCompanyDomesticWaybillPeriod, itemCompanyDomesticWaybill }
                                                                          ).SelectMany(itemCompanyDomesticWaybillPeriod => itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybill.DefaultIfEmpty(), (itemCompanyDomesticWaybillPeriod, itemCompanyDomesticWaybill) => new { itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod, itemCompanyDomesticWaybill })
                                                               .GroupJoin(
                                                                           dbContext.Companies,
                                                                           itemCompanyDomesticWaybillPeriod => itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.CompanyId,
                                                                           itemCompany => itemCompany.Id,
                                                                           (itemCompanyDomesticWaybillPeriod, itemCompany) => new { itemCompanyDomesticWaybillPeriod, itemCompany }
                                                                          ).SelectMany(itemCompanyDomesticWaybillPeriod => itemCompanyDomesticWaybillPeriod.itemCompany.DefaultIfEmpty(), (itemCompanyDomesticWaybillPeriod, itemCompany) => new { itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod, itemCompany })
                                                               .GroupBy(item => new
                                                               {
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.Active,
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.Code,
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.Description,
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.EndNumber,
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.Id,
                                                                   item.itemCompanyDomesticWaybillPeriod.itemCompanyDomesticWaybillPeriod.StartNumber,
                                                                   CompanyName = item.itemCompany!.Name,
                                                                   CompanyId = item.itemCompany!.Id,

                                                               }, (keys, group) => new CompanyDomesticWaybillPeriodGetAllDto
                                                               {
                                                                   Active = keys.Active,
                                                                   Code = keys.Code,
                                                                   Description = keys.Description,
                                                                   EndNumber = keys.EndNumber,
                                                                   Id = keys.Id,
                                                                   StartNumber = keys.StartNumber,
                                                                   CompanyName = keys.CompanyName,
                                                                   CompanyId = keys.CompanyId,
                                                                   CountReady = group.Count(itemCount => itemCount.itemCompanyDomesticWaybillPeriod!.itemCompanyDomesticWaybill!.State == (short)CompanyDomesticWaybillState.Ready),
                                                               })
                                                               .Where(item => item.Code.ToLower().Contains(searchPhrase) || item.Description.ToLower().Contains(searchPhrase));

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        baseQuery = hasReadyForm switch
        {
            1 => baseQuery.Where(item => item.CountReady > 0),
            0 => baseQuery.Where(item => item.CountReady == 0),
            _ => baseQuery
        };

        if (companyId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticWaybillPeriodGetAllDto, object>>>
        {
            { nameof(CompanyDomesticWaybillPeriodGetAllDto.Code), item => item.Code},
            { nameof(CompanyDomesticWaybillPeriodGetAllDto.StartNumber), item => item.StartNumber},
            { nameof(CompanyDomesticWaybillPeriodGetAllDto.Active), item => item.Active}
        };

        sortBy ??= nameof(CompanyDomesticWaybillPeriodGetAllDto.Code);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyDomesticWaybillPeriods = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyDomesticWaybillPeriods, totalCount);
    }

    public async Task<CompanyDomesticWaybillPeriod?> GetCompanyDomesticWaybillPeriodByCodeAsync(string companyDomesticWaybillPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybillPeriod> query = dbContext.CompanyDomesticWaybillPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyDomesticWaybillPeriodCode.Trim().ToLower() && item.CompanyId == companyId, cancellationToken);
    }
}
