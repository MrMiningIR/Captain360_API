using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class TokenBlacklist : BaseEntity
{

    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}