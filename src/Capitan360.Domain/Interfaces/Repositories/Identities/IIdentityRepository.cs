using Capitan360.Domain.Entities.Authorizations;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Repositories.Identities;

public interface IIdentityRepository
{

    Task<bool> UserExistByPhone(string phone, CancellationToken ct);

    //Task<Entities.Users.User> UserExistByUserId(string userId, CancellationToken ct);


    Task<IdentityResult?> CreateUserAsync(Entities.Users.User user, string password, CancellationToken ct);
    Task<IdentityResult?> UpdateUserAsync(Entities.Users.User user, CancellationToken ct);


    Task<IdentityResult?> AddRoleToUser(Entities.Users.User user, Role role);
    Task RemoveRoleFromUser(Entities.Users.User user);


    Task<Entities.Users.User?> FindUserByPhone(string phoneNumber, CancellationToken ct);

    // Users by Company Operations

    Task<IReadOnlyList<Entities.Users.User>> GetUsersByCompanyAsync(int companyId, CancellationToken ct);

    Task<(IReadOnlyList<Entities.Users.User>, int)> GetAllUsersByCompany(int companyId,
        int queryUserKind, int companyType, string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken ct);


    Task<UserCompany?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken ct);
    Task<Entities.Users.User?> GetUserByIdAsync(string userId, CancellationToken ct);
    Task<Entities.Users.User?> GetUserByIdForUpdateAsync(string userId, CancellationToken ct);
    Task<IdentityResult?> CreateUserByCompanyAsync(Entities.Users.User user, string password);
    Task<Entities.Users.User?> GetUserByPhoneNumberAndCompanyType(string phoneNumber, int companyType, CancellationToken cancellationToken);
    Task<Entities.Users.User?> GetUserByPhoneNumberAndCompanyTypeForUpdateOperation(string phoneNumber, int companyType,
        string userId, CancellationToken cancellationToken);
    Task<Entities.Users.User?> GetUserByPhoneNumberAndCompanyId(string phoneNumber, int companyId,
        CancellationToken cancellationToken);
    Task<Entities.Users.User?> GetUserByPhoneNumberAndCompanyIdForUpdateOperation(string phoneNumber, int companyId, string userId,
        CancellationToken cancellationToken);

    void DeleteRole(Role role);

}