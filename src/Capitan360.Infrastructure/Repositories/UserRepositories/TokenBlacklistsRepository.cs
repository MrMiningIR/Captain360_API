using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.UserRepositories;

internal class TokenBlacklistsRepository(ApplicationDbContext dbContext) : ITokenBlacklistsRepository
{

    public async Task<IReadOnlyList<TokenBlacklist>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.TokenBlacklists.ToListAsync(cancellationToken);
    }

    public async Task<TokenBlacklist?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var tokenBlacklist = await dbContext
            .TokenBlacklists.FirstOrDefaultAsync(t => t.Token == token, cancellationToken);

        return tokenBlacklist;


    }

    public async Task AddAsync(TokenBlacklist tokenBlacklist, CancellationToken cancellationToken = default)
    {
        await dbContext.TokenBlacklists.AddAsync(tokenBlacklist, cancellationToken);
    }
}