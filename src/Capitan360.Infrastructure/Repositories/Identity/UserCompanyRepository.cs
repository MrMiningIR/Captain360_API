using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identity;

public class UserCompanyRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IUserCompanyRepository
{
    public void DeleteUserCompany(UserCompany userCompany, CancellationToken cancellationToken)
    {
        dbContext.Entry(userCompany).Property("Deleted").CurrentValue = true;
    }

    public async Task<string> Create(UserCompany userCompany, CancellationToken cancellationToken)
    {
        dbContext.UserCompanies.Add(userCompany);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return userCompany.UserId;

    }

    public async Task<UserCompany?> GetUserCompanyByUserId(string userId, CancellationToken cancellationToken)
    {
        var userCompany = await dbContext.UserCompanies.SingleOrDefaultAsync(x => x.UserId == userId,
            cancellationToken: cancellationToken);
        return userCompany;
    }

    public async Task<UserCompany?> GetUserCompanyByCompanyIdAndUserId(string userId, int companyId, CancellationToken cancellationToken)
    {
        var userCompany = await dbContext.UserCompanies.SingleOrDefaultAsync(x => x.UserId == userId && x.CompanyId == companyId,
     cancellationToken: cancellationToken);
        return userCompany;
    }

    public async Task UpdateUserCompany(UserCompany userCompany, CancellationToken cancellationToken)
    {
        dbContext.UserCompanies.Update(userCompany);
        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}