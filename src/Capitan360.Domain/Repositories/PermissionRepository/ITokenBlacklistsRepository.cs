using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Repositories.PermissionRepository;

public interface ITokenBlacklistsRepository
{
    Task<IReadOnlyList<TokenBlacklist>> GetAllAsync(CancellationToken cancellationToken);
    Task<TokenBlacklist?> GetByTokenAsync(string token, CancellationToken cancellationToken);


}