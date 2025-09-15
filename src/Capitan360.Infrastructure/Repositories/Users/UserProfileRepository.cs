using Capitan360.Domain.Entities.Users;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Users;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Users;

public class UserProfileRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IUserProfileRepository
{
    public async Task<int> CreateUserProfile(UserProfile profile,
        CancellationToken cancellationToken)
    {
       

       
        dbContext.UserProfiles.Add(profile);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return profile.Id;
    }

    public async Task UpdateUserProfile(UserProfile profile, CancellationToken cancellationToken)
    {
        dbContext.UserProfiles.Update(profile);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void DeleteUserProfile(UserProfile profile, CancellationToken cancellationToken)
    {
        dbContext.Entry(profile).Property("Deleted").CurrentValue = true;
    }

    public async Task<UserProfile?> GetUserProfile(int id, string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserProfiles.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
    }

    public async Task<UserProfile?> GetUserProfile(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.UserProfiles.SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}

