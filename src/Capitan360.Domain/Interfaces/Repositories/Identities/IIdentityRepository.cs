using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IIdentityRepository
{

    Task<bool> UserExistByPhone(string phone, CancellationToken cancellationToken);

    //Task<Entities.Users.User> UserExistByUserId(string userId, CancellationToken cancellationToken);


    Task<IdentityResult?> CreateUserAsync(User user, string password, CancellationToken cancellationToken);
    Task<IdentityResult?> UpdateUserAsync(User user, CancellationToken cancellationToken);


    Task<IdentityResult?> AddRoleToUser(User user, Role role);
    Task RemoveRoleFromUser(User user);


    Task<User?> FindUserByPhone(string phoneNumber, CancellationToken cancellationToken);

    // Users by Company Operations

    Task<IReadOnlyList<User>> GetUsersByCompanyAsync(int companyId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<User>, int)> GetAllUsersByCompany(int companyId,
        int queryUserKind, int companyType, string searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);


    Task<UserCompany?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);
    Task<User?> GetUserByIdForUpdateAsync(string userId, CancellationToken cancellationToken);
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