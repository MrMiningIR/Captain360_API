using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Repositories.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identity;

public class RefreshTokenRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IRefreshTokenRepository
{
    public async Task AddRefreshToken(RefreshToken? encryptedToken, CancellationToken cancellationToken)
    {
        await dbContext.RefreshTokens.AddAsync(encryptedToken, cancellationToken);

    }

    public async Task<RefreshToken?> GetRefreshToken(string encryptedToken, CancellationToken cancellationToken)
    {
        return await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == encryptedToken && !t.IsRevoked && t.Expires > DateTime.UtcNow, cancellationToken);
    }

    public Task UpdateRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        dbContext.RefreshTokens.Remove(refreshToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenByUserIdAndSessionId(string userId, string sessionId, CancellationToken cancellationToken)
    {
        return await dbContext.RefreshTokens
             .FirstOrDefaultAsync(t =>
                     t.UserId == userId &&
                     t.SessionId == sessionId &&
                     !t.IsRevoked &&
                     t.Expires > DateTime.UtcNow,

                 cancellationToken: cancellationToken);
    }
}
