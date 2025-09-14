using Capitan360.Domain.Entities.Companies;
using System.Security.Claims;

namespace Capitan360.Domain.Repositories.Identities;

public interface ITokenService
{
    (string resultToken, DateTime validTo) GenerateAccessToken(List<Claim> claims, string jwtKey, string issuer, string audience);
    string GenerateRefreshToken();
    (string EncryptedToken, byte[] IV) EncryptRefreshToken(string token, string encryptionKey);
    string DecryptRefreshToken(string encryptedToken, byte[] iv, string encryptionKey);

    //List<Claim> ClaimsGenerator(Entities.Users.User user, IReadOnlyList<string> userGroups, IReadOnlyList<string> roles,
    //    string newSessionId);

    List<Claim> ClaimsGenerator(Entities.Users.User user, UserCompany userCompany, string permissionVersionControl,
        IReadOnlyList<string> roles,
        string newSessionId, List<string> permissions);
}