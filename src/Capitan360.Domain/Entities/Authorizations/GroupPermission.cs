using Capitan360.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace Capitan360.Domain.Entities.Authorizations;

public class GroupPermission : BaseEntity
{
    public int GroupId { get; set; }
    [JsonIgnore]
    public Group Group { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
