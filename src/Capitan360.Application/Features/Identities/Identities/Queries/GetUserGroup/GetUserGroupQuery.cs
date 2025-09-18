namespace Capitan360.Application.Features.Identities.Identities.Queries.GetUserGroup;

public record GetUserGroupQuery(string UserId, int GroupId)
{
    public string UserId { get; } = UserId;
    public int GroupId { get; } = GroupId;
};