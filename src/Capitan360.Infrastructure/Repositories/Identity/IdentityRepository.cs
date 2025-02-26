using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Repositories.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identity;

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

    public async Task AddToRole(User user, Role role)
    {

        await userManager.AddToRoleAsync(user, role.NormalizedName!);
    }

    public async Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetUsersByCompanyAsync(int companyId, CancellationToken cancellationToken)
    {
       return await dbContext.UserCompanies
            .Where(uc => uc.CompanyId == companyId)
            .Select(uc => uc.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.UserCompanies
            .Where(uc => uc.CompanyId == companyId && uc.UserId == userId)
            .Select(uc => uc.User)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IdentityResult?> CreateUserByCompanyAsync(User user, string password)
    {

       return  await userManager.CreateAsync(user, password);


    }
}

