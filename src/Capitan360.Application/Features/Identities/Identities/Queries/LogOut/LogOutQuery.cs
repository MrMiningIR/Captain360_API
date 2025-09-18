namespace Capitan360.Application.Features.Identities.Identities.Queries.LogOut;

public record LogOutQuery(string userId, string sessionId, string token)
{
    public string UserId { get; } = userId;
    public string SessionId { get; } = sessionId;
    public string Token { get; } = token;
}