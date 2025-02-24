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

    public async Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken)
    {

        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
            return false;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;


    }

    public async Task AddToRole(User user, Role role)
    {

        await userManager.AddToRoleAsync(user, role.NormalizedName!);
    }

    public async Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken: cancellationToken);
    }
}