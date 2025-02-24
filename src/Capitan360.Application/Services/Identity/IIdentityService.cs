using Capitan360.Application.Services.Identity.Users.Commands;
using Capitan360.Application.Services.Identity.Users.Queries;

namespace Capitan360.Application.Services.Identity;

public interface IIdentityService
{
    Task<bool>ExistPhone(string phone , CancellationToken cancellationToken);
    Task RegisterUser(CreateUserCommand registerUserDto, string roleName, CancellationToken cancellationToken);
    
    Task<LoginResponse> LoginUser(LoginUserQuery loginUserQuery , CancellationToken cancellationToken);

    Task<TokenResponse> RefreshToken(RefreshTokenQuery refreshTokenQuery, CancellationToken cancellationToken);

}