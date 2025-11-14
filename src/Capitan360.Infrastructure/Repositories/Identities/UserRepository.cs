using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            query = query.Include(item => item.Company).Include(x => x.Roles);

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

    public async Task<(IdentityResult Result, User? Created)> CreateUserAsync(User user, string password, Role userRole,
        CancellationToken cancellationToken)
    {
        var createResult = await userManager.CreateAsync(user, password);
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

    public async Task<(IReadOnlyList<User>, int)> GetAllUsersAsync(string? searchPhrase, string? sortBy, int companyId, int companyTypeId, string roleId, int typeOfFactorInSamanehMoadianId,
                                                                   int hasCredit, int baned, int active, int isBikeDelivery, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.Users.AsNoTracking();



        searchPhrase = searchPhrase.Trim().ToLower();


        baseQuery = baseQuery!.Where(item => item.NameFamily.ToLower().Contains(searchPhrase) || item.PhoneNumber!.ToLower().Contains(searchPhrase) ||
                           item.MobileTelegram.ToLower().Contains(searchPhrase) || item.Tell.ToLower().Contains(searchPhrase) ||
                           item.NationalCode.ToLower().Contains(searchPhrase) || item.EconomicCode.ToLower().Contains(searchPhrase) ||
                           item.NationalId.ToLower().Contains(searchPhrase) || item.RegistrationId.ToLower().Contains(searchPhrase) ||
                           item.EconomicCode.ToLower().Contains(searchPhrase) || item.EconomicCode.ToLower().Contains(searchPhrase));


        if (!string.IsNullOrEmpty(roleId))
            baseQuery = baseQuery.Where(role => role.Roles.Any(x => x.Id == roleId));



        if (loadData)
            baseQuery = baseQuery.Include(x => x.Roles).Include(item => item.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        if (companyTypeId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);
        }



        baseQuery = typeOfFactorInSamanehMoadianId switch
        {
            1 or 2 or 3 => baseQuery.Where(item => item.TypeOfFactorInSamanehMoadianId == typeOfFactorInSamanehMoadianId),
            _ => baseQuery
        };




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
            {  nameof(User.Tell), item => item.Tell }

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

    public async Task<User?> GetUserByIdAndCompanyId(string userId, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {

        IQueryable<User> query = dbContext.Users;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == userId && item.CompanyId == companyId, cancellationToken);



    }
}