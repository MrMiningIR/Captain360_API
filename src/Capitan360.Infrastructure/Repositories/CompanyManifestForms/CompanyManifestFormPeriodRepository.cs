using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyManifestForms;

public class CompanyManifestFormPeriodRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyManifestFormPeriodRepository
{
    public async Task<bool> CheckExistCompanyManifestFormPeriodCodeAsync(string companyManifestFormPeriodCode, int companyId, int? currentManifestFormPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestFormPeriods.AnyAsync(item => item.CompanyId == companyId && item.Code.ToLower() == companyManifestFormPeriodCode.Trim().ToLower() && (currentManifestFormPeriodId == null || item.Id != currentManifestFormPeriodId), cancellationToken);
    }

    public async Task<int> CreateCompanyManifestFormPeriodAsync(CompanyManifestFormPeriod companyManifestFormPeriod, CancellationToken cancellationToken)
    {
        dbContext.CompanyManifestFormPeriods.Add(companyManifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyManifestFormPeriod.Id;
    }

    public async Task<CompanyManifestFormPeriod?> GetCompanyManifestFormPeriodByIdAsync(int companyManifestFormPeriodId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyManifestFormPeriodId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyManifestFormPeriod>?> GetCompanyManifestFormPeriodByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyManifestFormPeriodAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken)
    {
        var companyManifestFormPeriod = await dbContext.CompanyManifestFormPeriods.SingleOrDefaultAsync(item => item.Id == companyManifestFormPeriodId, cancellationToken);
        if (companyManifestFormPeriod == null)
            return;

        dbContext.CompanyManifestFormPeriods.Remove(companyManifestFormPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyManifestFormPeriodGetAllDto>, int)> GetAllCompanyManifestFormPeriodsAsync(string searchPhrase, string? sortBy, int companyId, int active, int hasReadyForm, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.CompanyManifestFormPeriods.AsNoTracking()
                                                            .GroupJoin(
                                                                        dbContext.CompanyManifestForms,
                                                                        itemCompanyManifestFormPeriod => itemCompanyManifestFormPeriod.Id,
                                                                        itemCompanyManifestForm => itemCompanyManifestForm.CompanyManifestFormPeriodId,
                                                                        (itemCompanyManifestFormPeriod, itemCompanyManifestForm) => new { itemCompanyManifestFormPeriod, itemCompanyManifestForm }
                                                                       ).SelectMany(itemCompanyManifestFormPeriod => itemCompanyManifestFormPeriod.itemCompanyManifestForm.DefaultIfEmpty(), (itemCompanyManifestFormPeriod, itemCompanyManifestForm) => new { itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod, itemCompanyManifestForm })
                                                            .GroupJoin(
                                                                        dbContext.Companies,
                                                                        itemCompanyManifestFormPeriod => itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.CompanyId,
                                                                        itemCompany => itemCompany.Id,
                                                                        (itemCompanyManifestFormPeriod, itemCompany) => new { itemCompanyManifestFormPeriod, itemCompany }
                                                                       ).SelectMany(itemCompanyManifestFormPeriod => itemCompanyManifestFormPeriod.itemCompany.DefaultIfEmpty(), (itemCompanyManifestFormPeriod, itemCompany) => new { itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod, itemCompany })
                                                            .GroupBy(item => new
                                                            {
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.Active,
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.Code,
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.Description,
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.EndNumber,
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.Id,
                                                                item.itemCompanyManifestFormPeriod.itemCompanyManifestFormPeriod.StartNumber,
                                                                CompanyName = item.itemCompany!.Name,
                                                                CompanyId = item.itemCompany!.Id,
                                                            }, (keys, group) => new CompanyManifestFormPeriodGetAllDto
                                                            {
                                                                Active = keys.Active,
                                                                Code = keys.Code,
                                                                Description = keys.Description,
                                                                EndNumber = keys.EndNumber,
                                                                Id = keys.Id,
                                                                StartNumber = keys.StartNumber,
                                                                CompanyName = keys.CompanyName,
                                                                CompanyId = keys.CompanyId,
                                                                CountReady = group.Count(itemCount => itemCount.itemCompanyManifestFormPeriod!.itemCompanyManifestForm!.State == (short)CompanyManifestFormState.Ready),
                                                            })
                                                            .Where(item => searchPhrase == null || item.Code.ToLower().Contains(searchPhrase) || item.Description.ToLower().Contains(searchPhrase));

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

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyManifestFormPeriodGetAllDto, object>>>
        {
            { nameof(CompanyManifestFormPeriodGetAllDto.Code), item => item.Code},
            { nameof(CompanyManifestFormPeriodGetAllDto.StartNumber), item => item.StartNumber},
            { nameof(CompanyManifestFormPeriodGetAllDto.Active), item => item.Active}
        };

        sortBy ??= nameof(CompanyManifestFormPeriodGetAllDto.Code);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyManifestFormPeriods = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyManifestFormPeriods, totalCount);
    }

    public async Task<CompanyManifestFormPeriod?> GetCompanyManifestFormPeriodByCodeAsync(string companyManifestFormPeriodCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestFormPeriod> query = dbContext.CompanyManifestFormPeriods;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyManifestFormPeriodCode.Trim().ToLower() && item.CompanyId == companyId, cancellationToken);
    }
}
