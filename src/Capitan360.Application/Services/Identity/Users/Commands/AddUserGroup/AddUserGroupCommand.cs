namespace Capitan360.Application.Services.Identity.Users.Commands.AddUserGroup;

public record AddUserGroupCommand(string UserId, int GroupId)
{
    public string UserId { get; } = UserId;
    public int GroupId { get; } = GroupId;
};