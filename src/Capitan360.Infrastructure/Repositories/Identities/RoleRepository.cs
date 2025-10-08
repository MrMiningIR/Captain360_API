using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class RoleRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, RoleManager<Role> roleManager) : IRoleRepository
{
    public async Task<bool> CheckExistRoleNameAsync(string roleName, string? currentRoleId, CancellationToken cancellationToken)
    {
        return await dbContext.Roles.AnyAsync(item => item.Name!.ToLower() == roleName.Trim().ToLower() && (currentRoleId == null || item.Id.ToLower() != currentRoleId.Trim().ToLower()), cancellationToken);
    }

    public async Task<bool> CheckExistRolePersianNameAsync(string rolePersianName, string? currentRoleId, CancellationToken cancellationToken)
    {
        return await dbContext.Roles.AnyAsync(item => item.PersianName!.ToLower() == rolePersianName.Trim().ToLower() && (currentRoleId == null || item.Id.ToLower() != currentRoleId.Trim().ToLower()), cancellationToken);
    }

    public async Task<IdentityResult> CreateRoleAsync(Role role, CancellationToken cancellationToken)
    {
        var createResult = await roleManager.CreateAsync(role);
        return createResult;
    }

    public async Task<Role?> GetRoleByIdAsync(string roleId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<Role> query = dbContext.Roles;

        if (loadData)
        {
            //NO OP
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id.ToLower() == roleId.Trim().ToLower(), cancellationToken);
    }

    public async Task DeleteRoleAsync(string roleId, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<Role>, int)> GetAllRolesAsync(string searchPhrase, string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.Roles.AsNoTracking()
                                              .Where(item => item.Name!.ToLower().Contains(searchPhrase) || item.PersianName.ToLower().Contains(searchPhrase));

        if (loadData)
        {
            //NO OP
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Role, object>>>
        {
            { nameof(Role.Name), item => item.Name!},
            { nameof(Role.PersianName), item => item.PersianName}
        };

        sortBy ??= nameof(Role.PersianName);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var Roles = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (Roles, totalCount);
    }
}
