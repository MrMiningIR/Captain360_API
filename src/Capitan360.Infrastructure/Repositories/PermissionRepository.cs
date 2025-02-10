using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories;

internal class PermissionRepository(ApplicationDbContext dbContext) : IPermissionRepository
{
   

    public async Task<IReadOnlyList<Permission>> GetAllPolicy(CancellationToken cancellationToken)
    {
       return await dbContext.Permissions.ToListAsync(cancellationToken);
    }
}