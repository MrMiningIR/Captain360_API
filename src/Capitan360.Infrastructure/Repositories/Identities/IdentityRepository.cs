using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Entities.Users;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Identities;

internal class IdentityRepository(ApplicationDbContext dbContext, UserManager<User> userManager
    , IUnitOfWork unitOfWork, RoleManager<Role> roleManager) : IIdentityRepository
{
    public async Task<bool> UserExistByPhone(string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        return user != null;
    }

    public async Task<IdentityResult?> CreateUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        //user.UserName = user.PhoneNumber;
        var result = await userManager.CreateAsync(user, password);


        return result;


    }

    public async Task<IdentityResult?> UpdateUserAsync(User user, CancellationToken ct)
    {
        var result = await userManager.UpdateAsync(user);
        return result;
    }

    public async Task<IdentityResult?> AddRoleToUser(User user, Role role)
    {

        var result = await userManager.AddToRoleAsync(user, role.NormalizedName!);
        return result;
    }

    public async Task RemoveRoleFromUser(User user)
    {
        var currentRoles = await userManager.GetRolesAsync(user);
        if (currentRoles.Any())
        {
            await userManager.RemoveFromRolesAsync(user, currentRoles);

        }


    }

    public async Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken)
    {
        return await dbContext
                .Users
                .Include(u => u.UserCompanies)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken: cancellationToken)

            ;
    }

    public async Task<IReadOnlyList<User>> GetUsersByCompanyAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.UserCompanies
             .Where(uc => uc.CompanyId == companyId)
             .Select(uc => uc.User)
             .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<User>, int)> GetAllUsersByCompany(int companyId, int userKind, int companyType,
        string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();



        var baseQuery = dbContext.Users
            .AsNoTracking()
            .Where(x => companyType == 0 || x.CompanyType == companyType)
            .Include(x => x.Roles)
            .Include(x => x.UserCompanies)
            .ThenInclude(x => x.Company)
            .Where(u => (userKind == 0 || u.UserKind == userKind) &&
                        (searchPhraseLower == null ||
                         u.FullName!.ToLower().Contains(searchPhraseLower) ||
                         u.PhoneNumber!.Contains(searchPhraseLower)) &&
                        (companyId == 0 || dbContext.UserCompanies.Any(uc => uc.CompanyId == companyId && uc.UserId == u.Id)))
            .Select(u => u);





        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
            {
                { nameof(User.FullName), item => item.FullName ! },
                { nameof(User.Active), item => item.Active },
                { nameof(User.LastAccess), item => item.LastAccess }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var users = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (users, totalCount);
    }

    public async Task<UserCompany?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.UserCompanies
            .AsNoTracking()
            .Where(uc => uc.CompanyId == companyId && uc.UserId == userId)
            .Include(x => x.Company)
            .Include(x => x.User).
            ThenInclude(x => x.Profile)
            .Include(x => x.User)
            .ThenInclude(x => x.Roles)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    }

    public async Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users
                .AsNoTracking()
                .Include(x => x.Profile)
                .Include(x => x.UserCompanies)
                .ThenInclude(x => x.Company)
                .Include(x => x.Roles)

                .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken)

            ;
    }

    public async Task<User?> GetUserByIdForUpdateAsync(string userId, CancellationToken ct)
    {
        return await dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId, ct);
    }

    public async Task<IdentityResult?> CreateUserByCompanyAsync(User user, string password)
    {

        return await userManager.CreateAsync(user, password);


    }

    public async Task<User?> GetUserByPhoneNumberAndCompanyType(string phoneNumber, int companyType, CancellationToken cancellationToken)
    {
        return await dbContext.Users.SingleOrDefaultAsync(
            x => x.PhoneNumber == phoneNumber && x.CompanyType == companyType, cancellationToken);
    }

    public async Task<User?> GetUserByPhoneNumberAndCompanyTypeForUpdateOperation(string phoneNumber, int companyType, string userId,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.CompanyType == companyType, cancellationToken);

        if (user is not null)
        {
            if (user.Id != userId)
            {
                return user;
            }
        }
        return null;



    }

    public async Task<User?> GetUserByPhoneNumberAndCompanyId(string phoneNumber, int companyId, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
         .Include(u => u.UserCompanies)
         .Where(u => u.PhoneNumber == phoneNumber && u.UserCompanies.Any(uc => uc.CompanyId == companyId))
         .SingleOrDefaultAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetUserByPhoneNumberAndCompanyIdForUpdateOperation(string phoneNumber, int companyId, string userId,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.AsNoTracking()
 .Include(u => u.UserCompanies)
 .Where(u => u.PhoneNumber == phoneNumber && u.UserCompanies.Any(uc => uc.CompanyId == companyId))
 .SingleOrDefaultAsync(cancellationToken);
        if (user is not null)
        {
            if (user.Id != userId)
            {
                return user;
            }
        }
        return null;


    }

    public void DeleteRole(Role role)
    {
        dbContext.Entry(role).Property("Deleted").CurrentValue = true;
    }


}

