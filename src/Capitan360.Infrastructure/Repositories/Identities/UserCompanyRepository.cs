using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.Identities;

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

    public async Task<UserCompany?> GetUserCompanyByUserId(string userId, CancellationToken cancellationToken,
        bool tracked = true)
    {
        if (tracked)
        {
            var userCompany = await dbContext.UserCompanies.Include(x => x.Company)
                .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
            return userCompany;
        }
        else
        {
            var userCompany = await dbContext.UserCompanies.AsNoTracking().Include(x => x.Company).SingleOrDefaultAsync(x => x.UserId == userId,
            cancellationToken: cancellationToken);
            return userCompany;
        }
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