namespace Capitan360.Domain.Entities.Authorizations;

public class RoleGroup
{
    public string RoleId { get; set; }
    public Role Role { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
}