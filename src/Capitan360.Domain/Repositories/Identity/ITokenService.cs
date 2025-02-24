using Capitan360.Domain.Entities.UserEntity;
using System.Security.Claims;

namespace Capitan360.Domain.Repositories.Identity;

public interface ITokenService
{
    (string resultToken, DateTime validTo) GenerateAccessToken(User user, List<Claim> claims, string jwtKey, string issuer, string audience);
    string GenerateRefreshToken();
    (string EncryptedToken, byte[] IV) EncryptRefreshToken(string token, string encryptionKey);
    string DecryptRefreshToken(string encryptedToken, byte[] iv, string encryptionKey);

    List<Claim> ClaimsGenerator(User user, IReadOnlyList<string> userGroups, IReadOnlyList<string> roles,
        string newSessionId);
}