using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Companies.UserCompany.Commands.CreateUserCompany;
using Capitan360.Application.Features.Companies.UserCompany.Commands.UpdateUserCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserByCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserById;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUsersByCompany;
using Capitan360.Application.Features.Dtos;
using Capitan360.Application.Features.Identities.Identities.Commands.AddUserGroup;
using Capitan360.Application.Features.Identities.Identities.Commands.ChangePassword;
using Capitan360.Application.Features.Identities.Identities.Commands.ChangeUserActivity;
using Capitan360.Application.Features.Identities.Identities.Commands.CreateUser;
using Capitan360.Application.Features.Identities.Identities.Commands.UpdateUser;
using Capitan360.Application.Features.Identities.Identities.Dtos;
using Capitan360.Application.Features.Identities.Identities.Queries.GetUserGroup;
using Capitan360.Application.Features.Identities.Identities.Queries.LoginUser;
using Capitan360.Application.Features.Identities.Identities.Queries.LogOut;
using Capitan360.Application.Features.Identities.Identities.Queries.RefreshToken;
using Capitan360.Application.Features.Identities.Identities.Responses;

namespace Capitan360.Application.Features.Identities.Identities.Services;

public interface IIdentityService
{
    Task<bool> ExistPhone(string phone, CancellationToken cancellationToken);
    Task<ApiResponse<string>> RegisterUser(CreateUserCommand createUserCommand, CancellationToken cancellationToken);

    Task<ApiResponse<string>> UpdateUser(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken);

    Task<ApiResponse<LoginResponse>> LoginUser(LoginUserQuery loginUserQuery, CancellationToken cancellationToken);

    Task<ApiResponse<TokenResponse>> RefreshToken(RefreshTokenQuery refreshTokenQuery,
        CancellationToken cancellationToken);

    Task LogOutUser(LogOutQuery logOutQuery, CancellationToken cancellationToken);

    Task AddUserToGroup(AddUserGroupCommand addUserGroupCommand, CancellationToken cancellationToken);

    Task RemoveUserFromGroup(GetUserGroupQuery userGroupQuery, CancellationToken cancellationToken);



    // User By Company Operations
    Task<ApiResponse<PagedResult<UserDto>>> GetUsersByCompany(GetUsersByCompanyQuery getUserByCompanyQuery, CancellationToken cancellationToken);
    Task<ApiResponse<UserDto>> GetUserByCompany(GetUserByCompanyQuery getUsersByCompanyQuery,
        CancellationToken cancellationToken);
    Task<ApiResponse<UserDto>> GetUserById(GetUserByIdQuery getUserByIdQuery,
        CancellationToken cancellationToken);
    Task<ApiResponse<int>> CreateUserByCompany(CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken);
    Task UpdateUserCompany(UpdateUserCompanyCommand updateUserCompanyCommand, CancellationToken cancellationToken);


    ApiResponse<PagedResult<UserKindItemDto>> GetUserKindList();
    ApiResponse<PagedResult<MoadianItemDto>> GeMoadianList();
    ApiResponse<PagedResult<EntranceFeeTypeDto>> GetEntranceTypeList();
    ApiResponse<PagedResult<PathStructTypeDto>> GetPathStructTypeList();
    ApiResponse<PagedResult<WeightTypeDto>> GetWeightTypeList(List<int>? shouldRemove = null);
    ApiResponse<PagedResult<RateDto>> GetRateList();

    Task<ApiResponse<PagedResult<RoleDto>>> GetRoles(CancellationToken cancellationToken);
    Task<ApiResponse<List<CompanyItemDto>>> GetCompaniesList(int companyTypeId, CancellationToken cancellationToken);
    Task<ApiResponse<List<CityAreaDto>>> GetCityList(CancellationToken cancellationToken);

    Task<ApiResponse<string>> ChangePassword(ChangePasswordCommand changePasswordCommand);
    Task<ApiResponse<string>> SetUserActivityStatus(ChangeUserActivityCommand activityCommand, CancellationToken cancellationToken);





}