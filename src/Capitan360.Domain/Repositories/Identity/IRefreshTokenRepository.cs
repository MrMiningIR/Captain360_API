using Capitan360.Domain.Entities.AuthorizationEntity;
using System.Threading;

namespace Capitan360.Domain.Repositories.Identity;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken? refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task UpdateRefreshToken(RefreshToken refreshToken,CancellationToken cancellationToken);
    Task DeleteRefreshToken(string refreshToken, CancellationToken cancellationToken);

    Task<RefreshToken?> GetRefreshTokenByUserIdAndSessionId(string userId,string sessionId ,CancellationToken cancellationToken);
}