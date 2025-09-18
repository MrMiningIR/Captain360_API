using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Repositories.Identities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class TokenRepository
    (IUserCompanyRepository UserCompanyRepository)
    : ITokenRepository
{
    public (string resultToken, DateTime validTo) GenerateAccessToken(List<Claim> claims, string jwtKey, string issuer,
        string audience)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds);

        var resultToken = new JwtSecurityTokenHandler().WriteToken(token);

        return (resultToken, token.ValidTo);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public (string EncryptedToken, byte[] IV) EncryptRefreshToken(string token, string encryptionKey)
    {
        var key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32, '\0')); // مطمئن شو کلید 32 بایته
        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();
        var encryptor = aes.CreateEncryptor();
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var encrypted = encryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length);
        return (Convert.ToBase64String(encrypted), aes.IV);
    }

    public string DecryptRefreshToken(string encryptedToken, byte[] iv, string encryptionKey)
    {
        var key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32, '\0')); // همون کلید
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        var decryptor = aes.CreateDecryptor();
        var encryptedBytes = Convert.FromBase64String(encryptedToken);
        var decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        return Encoding.UTF8.GetString(decrypted);
    }

    public List<Claim> ClaimsGenerator(User user, UserCompany userCompany, string permissionVersionControl, IReadOnlyList<string> roles,
        string newSessionId, List<string> permissions)
    {

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.MobilePhone, user.PhoneNumber!),
            new(ClaimTypes.Name, user.FullName!) ,
            new(ConstantNames.CompanyId,  userCompany.CompanyId.ToString()),
            new(ConstantNames.IsParentCompany,  userCompany.Company.IsParentCompany.ToString()),
            new(ConstantNames.CompanyName,  userCompany.Company.Name.ToString()),
            new(ConstantNames.CompanyType,  user.CompanyType.ToString()),
            new(ConstantNames.Permissions,  string.Join(',',permissions))
        };

        if (!string.IsNullOrEmpty(newSessionId))
            claims.Add(new Claim(ConstantNames.SessionId, newSessionId));

        if (!string.IsNullOrEmpty(permissionVersionControl))

            claims.Add(new Claim(ConstantNames.Pvc, permissionVersionControl));

        if (roles.Any())
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
        return claims;
    }
}