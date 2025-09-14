using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Users;

public class TokenBlacklist : BaseEntity
{
   
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}