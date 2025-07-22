namespace Capitan360.Application.Services.Identity.Services;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}