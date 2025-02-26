using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Entities.UserEntity;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public DateTime LastAccess { get; set; }
    public string? ActivationCode { get; set; }
    public string? ActiveSessionId { get; set; }





    // Navigation Properties
    public UserProfile? UserProfile { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = [];
    public ICollection<UserCompany> UserCompanies { get; set; } = [];


}