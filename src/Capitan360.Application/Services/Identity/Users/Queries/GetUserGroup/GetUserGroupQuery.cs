namespace Capitan360.Application.Services.Identity.Users.Queries.GetUserGroup;

public record GetUserGroupQuery(string UserId, int GroupId)
{
    public string UserId { get; } = UserId;
    public int GroupId { get; } = GroupId;
};