using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IUserRepository
{

    Task<(IdentityResult Result, User? Created)> CreateUserAsync(User user, string password, Role userRole, CancellationToken cancellationToken);

    Task<bool> CheckExistUserMobileAsync(string mobile, int companyId, string? currentUserId, CancellationToken cancellationToken);

    Task<User?> GetUserByIdAsync(string userId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<User?> GetUserByMobileAndCompanyIdAsync(string mobile, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteUserAsync(string userId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<User>, int)> GetAllUsersAsync(string? searchPhrase, string? sortBy, int companyId,
        int companyTypeId, string roleId, int typeOfFactorInSamanehMoadianId,
        int hasCredit, int baned, int active, int isBikeDelivery, bool loadData, int pageNumber, int pageSize,
        SortDirection sortDirection, CancellationToken cancellationToken);

    Task<User?> GetUserByIdAndCompanyId(string queryUserId, int queryCompanyId, bool loadData,
        bool tracked,
        CancellationToken cancellationToken);
}