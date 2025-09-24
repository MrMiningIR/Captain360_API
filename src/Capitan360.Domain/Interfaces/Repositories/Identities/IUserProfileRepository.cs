using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IUserProfileRepository
{
    Task<int> CreateUserProfile(UserProfile profile, CancellationToken cancellationToken);
    Task UpdateUserProfile(UserProfile profile, CancellationToken cancellationToken);
    void DeleteUserProfile(UserProfile profile, CancellationToken cancellationToken);

    Task<UserProfile?> GetUserProfile(int id, string userId, CancellationToken cancellationToken);
    Task<UserProfile?> GetUserProfile(string userId, CancellationToken cancellationToken);
}