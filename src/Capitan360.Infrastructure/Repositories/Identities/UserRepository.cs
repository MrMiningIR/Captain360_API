using Capitan360.Domain.Interfaces;
using Capitan360.Infrastructure.Persistence;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Capitan360.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager, IUnitOfWork unitOfWork) : IUserRepository
{
    public async Task<bool> CheckExistUserMobileAsync(string mobile, int companyId, string? currentUserId, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AnyAsync(item => item.PhoneNumber!.ToLower() == mobile.Trim().ToLower() && item.CompanyId == companyId && (currentUserId == null || item.Id.ToLower() != currentUserId.Trim().ToLower()), cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(string userId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<User> query = dbContext.Users;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id.ToLower() == userId.Trim().ToLower(), cancellationToken);
    }

    public async Task<User?> GetUserByMobileAndCompanyIdAsync(string mobile, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<User> query = dbContext.Users;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.PhoneNumber!.ToLower() == mobile.Trim().ToLower() && item.CompanyId == companyId, cancellationToken);
    }

    public async Task<(IdentityResult Result, User? Created)> CreateUserAsync(User user, Role userRole, CancellationToken cancellationToken)
    {
        var createResult = await userManager.CreateAsync(user);
        if (!createResult.Succeeded)
            return (createResult, null);

        var addToRoleResult = await userManager.AddToRoleAsync(user, userRole.NormalizedName!);
        if (!addToRoleResult.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return (addToRoleResult, null);
        }
        return (IdentityResult.Success, user);
    }

    public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<User>, int)> GetAllUsersAsync(string searchPhrase, string? sortBy, int companyId, int companyTypeId, int roleId, int typeOfFactorInSamanehMoadianId,
                                                                   int hasCredit, int baned, int active, int isBikeDelivery, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.Users.AsNoTracking()
                                       .Where(item => item.NameFamily.ToLower().Contains(searchPhrase) || item.PhoneNumber!.ToLower().Contains(searchPhrase) ||
                                                      item.MobileTelegram.ToLower().Contains(searchPhrase) || item.Tell.ToLower().Contains(searchPhrase) ||
                                                      item.NationalCode.ToLower().Contains(searchPhrase) || item.EconomicCode.ToLower().Contains(searchPhrase) ||
                                                      item.NationalId.ToLower().Contains(searchPhrase) || item.RegistrationId.ToLower().Contains(searchPhrase) ||
                                                      item.EconomicCode.ToLower().Contains(searchPhrase) || item.EconomicCode.ToLower().Contains(searchPhrase));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        if (companyTypeId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);
        }

        if (roleId != 0)
        {
            baseQuery = baseQuery.Where(item => item.RoleId == roleId);
        }

        if (typeOfFactorInSamanehMoadianId != -1)
        {
            baseQuery = baseQuery.Where(item => item.TypeOfFactorInSamanehMoadianId == typeOfFactorInSamanehMoadianId);
        }

        baseQuery = hasCredit switch
        {
            1 => baseQuery.Where(item => item.HasCredit),
            0 => baseQuery.Where(item => !item.HasCredit),
            _ => baseQuery
        };

        baseQuery = baned switch
        {
            1 => baseQuery.Where(item => item.Baned),
            0 => baseQuery.Where(item => !item.Baned),
            _ => baseQuery
        };

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        baseQuery = isBikeDelivery switch
        {
            1 => baseQuery.Where(item => item.IsBikeDelivery),
            0 => baseQuery.Where(item => !item.IsBikeDelivery),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
        {
            { nameof(User.NameFamily), item => item.NameFamily },
            {  nameof(User.PhoneNumber), item => item.PhoneNumber! },
            {  nameof(User.MobileTelegram), item => item.MobileTelegram },
            {  nameof(User.Credit), item => item.Credit },
            {  nameof(User.Active), item => item.Active },
            {  nameof(User.Baned), item => item.Baned}
        };

        sortBy ??= nameof(User.NameFamily);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);

        var ContentTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (ContentTypes, totalCount);
    }
}