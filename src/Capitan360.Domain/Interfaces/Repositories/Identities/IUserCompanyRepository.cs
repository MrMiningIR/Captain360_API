using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Interfaces.Repositories.Identities;

public interface IUserCompanyRepository
{
    Task<UserCompany?> GetUserCompanyByUserId(string userId, CancellationToken cancellationToken, bool tracked = true);
    Task<UserCompany?> GetUserCompanyByCompanyIdAndUserId(string userId, int companyId, CancellationToken cancellationToken);
    Task UpdateUserCompany(UserCompany userCompany, CancellationToken cancellationToken);
    void DeleteUserCompany(UserCompany userCompany, CancellationToken cancellationToken);
    Task<string> Create(UserCompany userCompany, CancellationToken cancellationToken);
}