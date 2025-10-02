using System.Linq.Expressions;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticPaths;

public class CompanyDomesticPathReceiverCompanyRepository(
    ApplicationDbContext dbContext,
    IUnitOfWork unitOfWork) : ICompanyDomesticPathReceiverCompanyRepository
{
    public async Task<bool> CheckExistCompanyDomesticPathReceiverCompanyAsync(int companyDomesticPathId, int municipalAreaId, int? currentCompanyDomesticPathReceiverCompanyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathReceiverCompanies.AnyAsync(cdp => cdp.CompanyDomesticPathId == companyDomesticPathId && (currentCompanyDomesticPathReceiverCompanyId == null || cdp.Id != currentCompanyDomesticPathReceiverCompanyId) &&
                                                                                    cdp.MunicipalAreaId == municipalAreaId, cancellationToken);
    }

    public async Task<int> CreateCompanyDomesticPathCompanyReceiverAsync(CompanyDomesticPathReceiverCompany companyDomesticPathReceiverCompany, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPathReceiverCompanies.Add(companyDomesticPathReceiverCompany);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticPathReceiverCompany.Id;
    }

    public async Task<CompanyDomesticPathReceiverCompany?> GetCompanyDomesticPathReceiverCompanyByIdAsync(int companyDomesticPathReceiverCompanyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticPathReceiverCompany> query = dbContext.CompanyDomesticPathReceiverCompanies;

        if (loadData)
        {
            query = query.Include(a => a.ReceiverCompany);
            query = query.Include(a => a.MunicipalArea);
            query = query.Include(a => a.CompanyDomesticPath);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(a => a.Id == companyDomesticPathReceiverCompanyId, cancellationToken);
    }

    public async Task DeleteCompanyDomesticPathReceiverCompanyAsync(int companyDomesticPathReceiverCompanyId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyDomesticPathReceiverCompany>, int)> GetAllCompanyDomesticPathReceiverCompaniesAsync(string searchPhrase, string? sortBy, int companyPathId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.ToLower();
        var baseQuery = dbContext.CompanyDomesticPathReceiverCompanies.AsNoTracking()
                                                                      .Where(item => item.CompanyDomesticPathId == companyPathId &&
                                                                                   (item.ReceiverCompany!.Name.Contains(searchPhrase) ||
                                                                                    item.ReceiverCompany.Code.Contains(searchPhrase) ||
                                                                                    item.MunicipalArea!.PersianName.Contains(searchPhrase) ||
                                                                                    item.ReceiverCompanyUserInsertedAddress!.Contains(searchPhrase) ||
                                                                                    item.ReceiverCompanyUserInsertedCode!.Contains(searchPhrase) ||
                                                                                    item.ReceiverCompanyUserInsertedName!.Contains(searchPhrase) ||
                                                                                    item.ReceiverCompanyUserInsertedTelephone!.Contains(searchPhrase) ));

        if (loadData || true)//چون CompanyName توی لیست مرتب سازی میاد برای همین باید همیشه لود دیتا انجام شود
        {
            baseQuery = baseQuery.Include(a => a.ReceiverCompany);
            baseQuery = baseQuery.Include(a => a.MunicipalArea);
            baseQuery = baseQuery.Include(a => a.CompanyDomesticPath);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPathReceiverCompany, object>>>
            {
                { nameof(CompanyDomesticPathReceiverCompany.MunicipalArea.PersianName), cdp => cdp.MunicipalArea!.PersianName}
            };

        sortBy ??= nameof(CompanyDomesticPathReceiverCompany.MunicipalArea.PersianName);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyDomesticPathReceivers = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyDomesticPathReceivers, totalCount);
    }

    public async Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByDomesticPathIdAsync(int companyDomesticPathId, CancellationToken cancellationToken)
    {
        var query = dbContext.CompanyDomesticPathReceiverCompanies.Include(a => a.MunicipalArea)
                                                                  .Include(a => a.ReceiverCompany)
                                                                  .Include(a => a.CompanyDomesticPath)
                                                                  .AsNoTracking();

        return await query.Where(item => item.CompanyDomesticPathId == companyDomesticPathId)
                          .OrderBy(cdp => cdp.MunicipalArea!.PersianName)
                          .ToListAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyDomesticPathReceiverCompanyByCompanySenderIdAndCompanyReceiverCodeAsync(int companySenderId, string receiverCompanyUserInsertedCode, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPathReceiverCompanies.Join(dbContext.CompanyDomesticPaths,
                                                                         companyDomesticPathReceiverItem => companyDomesticPathReceiverItem.CompanyDomesticPathId,
                                                                         companyDomesticPathItem => companyDomesticPathItem.Id,
                                                                         (companyDomesticPathReceiverItem, companyDomesticPathItem) => new { companyDomesticPathReceiverItem, companyDomesticPathItem })
                                                                   .AnyAsync(item => item.companyDomesticPathItem.CompanyId == companySenderId && 
                                                                                     item.companyDomesticPathReceiverItem.ReceiverCompanyId == null && item.companyDomesticPathReceiverItem.ReceiverCompanyUserInsertedCode != null && item.companyDomesticPathReceiverItem.ReceiverCompanyUserInsertedCode.ToLower() == receiverCompanyUserInsertedCode.Trim().ToLower(), cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndCompanyReceiverCodeAsync(int companySenderId, string receiverCompanyUserInsertedCode, CancellationToken cancellationToken)
    {
        var query = dbContext.CompanyDomesticPathReceiverCompanies.Join(dbContext.CompanyDomesticPaths,
                                                                        companyDomesticPathReceiverItem => companyDomesticPathReceiverItem.CompanyDomesticPathId,
                                                                        companyDomesticPathItem => companyDomesticPathItem.Id,
                                                                        (companyDomesticPathReceiverItem, companyDomesticPathItem) => new { companyDomesticPathReceiverItem, companyDomesticPathItem })
                                                                  .Where(item => item.companyDomesticPathItem.CompanyId == companySenderId &&
                                                                                item.companyDomesticPathReceiverItem.ReceiverCompanyId == null && item.companyDomesticPathReceiverItem.ReceiverCompanyUserInsertedCode != null && item.companyDomesticPathReceiverItem.ReceiverCompanyUserInsertedCode == receiverCompanyUserInsertedCode)
                                                                  .Select(item => item.companyDomesticPathReceiverItem);

        return await query.AsNoTracking()
                          .Include(a => a.ReceiverCompany)
                          .Include(a => a.MunicipalArea)
                          .Include(a => a.CompanyDomesticPath)
                          .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndReceiverCompanyIdAsync(int companySenderId, int companyReceiverId, CancellationToken cancellationToken)
    {
        var query = dbContext.CompanyDomesticPathReceiverCompanies.Join(dbContext.CompanyDomesticPaths,
                                                                        companyDomesticPathReceiverItem => companyDomesticPathReceiverItem.CompanyDomesticPathId,
                                                                        companyDomesticPathItem => companyDomesticPathItem.Id,
                                                                        (companyDomesticPathReceiverItem, companyDomesticPathItem) => new { companyDomesticPathReceiverItem, companyDomesticPathItem })
                                                                  .Where(item => item.companyDomesticPathItem.CompanyId == companySenderId &&
                                                                                item.companyDomesticPathReceiverItem.ReceiverCompanyId != null && item.companyDomesticPathReceiverItem.ReceiverCompanyId == companyReceiverId)
                                                                  .Select(item => item.companyDomesticPathReceiverItem);

        return await query.AsNoTracking()
                          .Include(a => a.ReceiverCompany)
                          .Include(a => a.MunicipalArea)
                          .Include(a => a.CompanyDomesticPath)
                          .ToListAsync(cancellationToken);
    }
}