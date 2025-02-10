using Capitan360.Domain.Entities.AuthorizationEntity;

namespace Capitan360.Domain.Repositories.PermissionRepository;

public interface IPermissionRepository
{
    Task<IReadOnlyList<Permission>> GetAllPolicy(CancellationToken cancellationToken);
}