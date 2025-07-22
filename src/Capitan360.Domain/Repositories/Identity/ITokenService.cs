using System.Security.Claims;

namespace Capitan360.Domain.Repositories.Identity;

public interface ITokenService
{
    (string resultToken, DateTime validTo) GenerateAccessToken(List<Claim> claims, string jwtKey, string issuer, string audience);
    string GenerateRefreshToken();
    (string EncryptedToken, byte[] IV) EncryptRefreshToken(string token, string encryptionKey);
    string DecryptRefreshToken(string encryptedToken, byte[] iv, string encryptionKey);

    //List<Claim> ClaimsGenerator(Entities.UserEntity.User user, IReadOnlyList<string> userGroups, IReadOnlyList<string> roles,
    //    string newSessionId);

    List<Claim> ClaimsGenerator(Entities.UserEntity.User user, string permissionVersionControl,
        IReadOnlyList<string> roles,
        string newSessionId, List<string> permissions);
}