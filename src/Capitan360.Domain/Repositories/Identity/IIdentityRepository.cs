using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Repositories.Identity;

public interface IIdentityRepository
{

    Task<bool> UserExistByPhone(string phone, CancellationToken ct);

    //Task<Entities.UserEntity.User> UserExistByUserId(string userId, CancellationToken ct);


    Task<IdentityResult?> CreateUserAsync(Entities.UserEntity.User user, string password, CancellationToken ct);
    Task<IdentityResult?> UpdateUserAsync(Entities.UserEntity.User user, CancellationToken ct);


    Task<IdentityResult?> AddRoleToUser(Entities.UserEntity.User user, Role role);
    Task RemoveRoleFromUser(Entities.UserEntity.User user);


    Task<Entities.UserEntity.User?> FindUserByPhone(string phoneNumber, CancellationToken ct);

    // Users by Company Operations

    Task<IReadOnlyList<Entities.UserEntity.User>> GetUsersByCompanyAsync(int companyId, CancellationToken ct);

    Task<(IReadOnlyList<Entities.UserEntity.User>, int)> GetMatchingAllUsersByCompany(int companyId,
        int queryUserKind, int companyType, string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken ct);


    Task<UserCompany?> GetUserByCompanyAsync(string userId, int companyId, CancellationToken ct);
    Task<Entities.UserEntity.User?> GetUserByIdAsync(string userId, CancellationToken ct);
    Task<Entities.UserEntity.User?> GetUserByIdForUpdateAsync(string userId, CancellationToken ct);
    Task<IdentityResult?> CreateUserByCompanyAsync(Entities.UserEntity.User user, string password);
    Task<Entities.UserEntity.User?> GetUserByPhoneNumberAndCompanyType(string phoneNumber, int companyType, CancellationToken cancellationToken);
    Task<Entities.UserEntity.User?> GetUserByPhoneNumberAndCompanyTypeForUpdateOperation(string phoneNumber, int companyType,
        string userId, CancellationToken cancellationToken);
    Task<Entities.UserEntity.User?> GetUserByPhoneNumberAndCompanyId(string phoneNumber, int companyId,
        CancellationToken cancellationToken);
    Task<Entities.UserEntity.User?> GetUserByPhoneNumberAndCompanyIdForUpdateOperation(string phoneNumber, int companyId, string userId,
        CancellationToken cancellationToken);

    void DeleteRole(Role role);

}