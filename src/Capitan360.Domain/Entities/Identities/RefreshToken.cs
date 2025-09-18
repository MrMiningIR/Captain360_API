using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public byte[] IV { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
    public string ClientIp { get; set; }
    public string SessionId { get; set; }

    public string UserId { get; set; }
    public User User { get; set; } = default!;
}
