namespace Capitan360.Domain.Repositories.Identity;

public interface IUserGroupRepository
{
    Task<IReadOnlyList<string>>GetUserGroupNameListAsyncByUserId(string userId, CancellationToken cancellationToken);
}