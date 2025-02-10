using System.Text.Json.Serialization;

namespace Capitan360.Domain.Entities.AuthorizationEntity
{
    public class GroupPermission
    {
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
