using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Capitan360.Infrastructure.Authorization.Services;

public class JwtService(IConfiguration configuration)
{
    private readonly string _key = configuration["Jwt:Key"] ?? throw new InvalidOperationException();
    private readonly string _issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException();
    private readonly string _audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException();
    private readonly int _expireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"] ?? throw new InvalidOperationException());

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
        };

        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
}

