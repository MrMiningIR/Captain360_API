using System.Linq.Expressions;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;

namespace Capitan360.Infrastructure.Repositories.Companies;

internal class CompanyBankRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyBankRepository
{
    public async Task<bool> CheckExistCompanyBankNameAsync(string companyBankName, int companyId, int? currentCompanyBankId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyBanks.AnyAsync(item => item.Name.ToLower() == companyBankName.ToLower().Trim() && item.CompanyId == companyId && (currentCompanyBankId == null || item.Id != currentCompanyBankId), cancellationToken);
    }

    public async Task<bool> CheckExistCompanyBankCodeAsync(string companyBankCode, int companyId, int? currentCompanyBankId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyBanks.AnyAsync(item => item.Code.ToLower() == companyBankCode.ToLower().Trim() && item.CompanyId == companyId && (currentCompanyBankId == null || item.Id != currentCompanyBankId), cancellationToken);
    }

    public async Task<int> GetCountCompanyBankAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyBanks.CountAsync(item => item.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task<int> CreateCompanyBankAsync(CompanyBank companyBank, CancellationToken cancellationToken)
    {
        dbContext.CompanyBanks.Add(companyBank);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyBank.Id;
    }

    public async Task<CompanyBank?> GetCompanyBankByIdAsync(int companyBankId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyBank> query = dbContext.CompanyBanks;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyBankId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyBank>?> GetCompanyBankByCompanyIdAsync(int companyBankCompanyId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyBank> query = dbContext.CompanyBanks;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.Where(item => item.CompanyId == companyBankCompanyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyBankAsync(int cmpanyBankId)
    {
        await Task.Yield();
    }

    public async Task MoveCompanyBankUpAsync(int companyBankId, CancellationToken cancellationToken)
    {
        var currentCompanyBank = await dbContext.CompanyBanks.SingleOrDefaultAsync(item => item.Id == companyBankId, cancellationToken: cancellationToken);
        if (currentCompanyBank == null)
            return;

        var nextCompanyBank = await dbContext.CompanyBanks.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyBank!.CompanyId && item.Order == currentCompanyBank.Order - 1, cancellationToken: cancellationToken);
        if (nextCompanyBank == null)
            return;

        nextCompanyBank!.Order++;
        currentCompanyBank!.Order--;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyBankDownAsync(int companyBankId, CancellationToken cancellationToken)
    {
        var currentCompanyBank = await dbContext.CompanyBanks.SingleOrDefaultAsync(item => item.Id == companyBankId, cancellationToken: cancellationToken);
        if (currentCompanyBank == null)
            return;

        var nextCompanyBank = await dbContext.CompanyBanks.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyBank!.CompanyId && item.Order == currentCompanyBank.Order + 1, cancellationToken: cancellationToken);
        if (nextCompanyBank == null)
            return;

        nextCompanyBank!.Order--;
        currentCompanyBank!.Order++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyBank>, int)> GetAllCompanyBanksAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyBanks.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.Name.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyBank, object>>>
        {
            { nameof(CompanyBank.Order), item => item.Order }
        };

        sortBy ??= nameof(CompanyBank.Order);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var Banks = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (Banks, totalCount);
    }

    public async Task<CompanyBank?> GetCompanyBankByCodeAsync(string companyBankCode, int companyId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<CompanyBank> query = dbContext.CompanyBanks;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Code.ToLower() == companyBankCode.ToLower().Trim() && item.CompanyId == companyId, cancellationToken);
    }
}
