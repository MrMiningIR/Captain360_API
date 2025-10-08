using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IRoleRepository
{
    Task<bool> CheckExistRoleNameAsync(string roleName, string? currentRoleId, CancellationToken cancellationToken);

    Task<bool> CheckExistRolePersianNameAsync(string rolePersianName, string? currentRoleId, CancellationToken cancellationToken);

    Task<IdentityResult> CreateRoleAsync(Role role, CancellationToken cancellationToken);

    Task<Role?> GetRoleByIdAsync(string roleId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Role>, int)> GetAllRolesAsync(string searchPhrase, string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);
}