namespace Capitan360.Application.Features.Identities.Identities.Services;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}