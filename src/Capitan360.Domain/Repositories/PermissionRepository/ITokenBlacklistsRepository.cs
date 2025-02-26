using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Repositories.PermissionRepository;

public interface ITokenBlacklistsRepository
{
    Task<IReadOnlyList<TokenBlacklist>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TokenBlacklist?> GetByTokenAsync(string token, CancellationToken cancellationToken=default);

    Task AddAsync(TokenBlacklist tokenBlacklist, CancellationToken cancellationToken = default);


}