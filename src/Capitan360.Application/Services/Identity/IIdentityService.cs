using Capitan360.Application.Services.Identity.Users.Commands.AddUserGroup;
using Capitan360.Application.Services.Identity.Users.Commands.CreateUser;
using Capitan360.Application.Services.Identity.Users.Queries.GetUserGroup;
using Capitan360.Application.Services.Identity.Users.Queries.LoginUser;
using Capitan360.Application.Services.Identity.Users.Queries.LogOut;
using Capitan360.Application.Services.Identity.Users.Queries.RefreshToken;
using Capitan360.Application.Services.Identity.Users.Responses;
using Capitan360.Application.Services.UserCompany;
using Capitan360.Application.Services.UserCompany.Commands.Create;
using Capitan360.Application.Services.UserCompany.Commands.Update;
using Capitan360.Application.Services.UserCompany.Queries;
using Capitan360.Domain.Entities.AuthorizationEntity;

namespace Capitan360.Application.Services.Identity;

public interface IIdentityService
{
    Task<bool> ExistPhone(string phone, CancellationToken cancellationToken);
    Task RegisterUser(CreateUserCommand createUserCommand, string roleName, CancellationToken cancellationToken);

    Task<LoginResponse> LoginUser(LoginUserQuery loginUserQuery, CancellationToken cancellationToken);

    Task<TokenResponse> RefreshToken(RefreshTokenQuery refreshTokenQuery, CancellationToken cancellationToken);

    Task LogOutUser(LogOutQuery logOutQuery, CancellationToken cancellationToken);

    Task AddUserToGroup(AddUserGroupCommand addUserGroupCommand, CancellationToken cancellationToken);

    Task RemoveUserFromGroup(GetUserGroupQuery userGroupQuery, CancellationToken cancellationToken);
    


    // User By Company Operations
    Task<IReadOnlyList<UserDto>> GetUsersByCompany(GetUsersByCompanyQuery getUserByCompanyQuery, CancellationToken cancellationToken);
    Task<UserDto?> GetUserByCompany(GetUserByCompanyQuery getUsersByCompanyQuery, CancellationToken cancellationToken);
    Task<string> CreateUserByCompany(CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken);
    Task UpdateUserCompany(UpdateUserCompanyCommand updateUserCompanyCommand, CancellationToken cancellationToken);
}