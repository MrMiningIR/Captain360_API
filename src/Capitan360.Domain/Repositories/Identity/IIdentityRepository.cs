using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Repositories.Identity;

public interface IIdentityRepository
{

    Task<bool> UserExistByPhone(string phone, CancellationToken cancellationToken);


    Task<IdentityResult?> CreateUserAsync(User user, string password, CancellationToken cancellationToken);

    Task AddToRole(User user, Role role);

    Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken);

    // Users by Company Operations

    Task<IReadOnlyList<User>> GetUsersByCompanyAsync(int companyId, CancellationToken cancellationToken);
    Task<User?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken cancellationToken);
    Task<IdentityResult?> CreateUserByCompanyAsync(User user, string password);
}