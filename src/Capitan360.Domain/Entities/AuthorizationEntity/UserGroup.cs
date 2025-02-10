using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Entities.AuthorizationEntity;

public class UserGroup
{
    public string UserId { get; set; }
    public User User { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
}