using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IIdentityRepository
{

    Task<bool> UserExistByPhone(string phone, CancellationToken ct);

    //Task<Entities.Users.User> UserExistByUserId(string userId, CancellationToken ct);


    Task<IdentityResult?> CreateUserAsync(User user, string password, CancellationToken ct);
    Task<IdentityResult?> UpdateUserAsync(User user, CancellationToken ct);


    Task<IdentityResult?> AddRoleToUser(User user, Role role);
    Task RemoveRoleFromUser(User user);


    Task<User?> FindUserByPhone(string phoneNumber, CancellationToken ct);

    // Users by Company Operations

    Task<IReadOnlyList<User>> GetUsersByCompanyAsync(int companyId, CancellationToken ct);

    Task<(IReadOnlyList<User>, int)> GetAllUsersByCompany(int companyId,
        int queryUserKind, int companyType, string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken ct);


    Task<UserCompany?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken ct);
    Task<User?> GetUserByIdAsync(string userId, CancellationToken ct);
    Task<User?> GetUserByIdForUpdateAsync(string userId, CancellationToken ct);
    Task<IdentityResult?> CreateUserByCompanyAsync(User user, string password);
    Task<User?> GetUserByPhoneNumberAndCompanyType(string phoneNumber, int companyType, CancellationToken cancellationToken);
    Task<User?> GetUserByPhoneNumberAndCompanyTypeForUpdateOperation(string phoneNumber, int companyType,
        string userId, CancellationToken cancellationToken);
    Task<User?> GetUserByPhoneNumberAndCompanyId(string phoneNumber, int companyId,
        CancellationToken cancellationToken);
    Task<User?> GetUserByPhoneNumberAndCompanyIdForUpdateOperation(string phoneNumber, int companyId, string userId,
        CancellationToken cancellationToken);

    void DeleteRole(Role role);

}