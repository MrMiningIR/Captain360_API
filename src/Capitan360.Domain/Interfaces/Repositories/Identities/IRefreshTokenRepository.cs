
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Repositories.Identities;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken? refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task UpdateRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken);

    Task<RefreshToken?> GetRefreshTokenByUserIdAndSessionId(string userId, string sessionId, CancellationToken cancellationToken);
}