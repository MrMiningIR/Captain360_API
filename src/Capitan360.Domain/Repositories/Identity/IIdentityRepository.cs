using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Repositories.Identity;

public interface IIdentityRepository
{

    Task<bool>UserExistByPhone(string phone , CancellationToken cancellationToken);


    Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken);

    Task AddToRole(User user, Role role);


    Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken);
}