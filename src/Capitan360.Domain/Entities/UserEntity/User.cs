using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Entities.UserEntity;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public string? CapitanCargoCode { get; set; }
    public bool Active { get; set; }
    public DateTime LastAccess { get; set; }
    public string? ActivationCode { get; set; }
    public DateTime ActivationCodeExpireTime { get; set; }

    public string? ActiveSessionId { get; set; }



    // Navigation Properties
    public UserProfile Profile { get; set; } = default!;

    public ICollection<UserGroup> UserGroups { get; set; } = [];
    public ICollection<UserCompany> UserCompanies { get; set; } = [];


}