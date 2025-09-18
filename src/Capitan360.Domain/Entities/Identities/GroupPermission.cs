using System.Text.Json.Serialization;
using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class GroupPermission : BaseEntity
{
    public int GroupId { get; set; }
    [JsonIgnore]
    public Group Group { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
