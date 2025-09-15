using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyAddressRepository(ApplicationDbContext dbContext) : ICompanyAddressRepository
{
    //public async Task<CompanyAddress?> GetByIdAsync(int addressId, CancellationToken cancellationToken)
    //{
    //    return await dbContext.CompanyAddresses.FirstOrDefaultAsync(ad => ad.AddressId == addressId, cancellationToken);

    //}

    //public async Task<int> OrderAddress(int commandCompanyId, CancellationToken cancellationToken)
    //{
    //    //return await dbContext.CompanyAddresses
    //    //    .Where(item => item.CompanyId == commandCompanyId)
    //    //    .OrderByDescending(item => item.OrderAddress)
    //    //    .Select(item => item.OrderAddress)
    //    //    .FirstOrDefaultAsync(cancellationToken);


    //    var maxOrder = await dbContext.CompanyAddresses
    //        .Where(item => item.CompanyId == commandCompanyId)
    //        .MaxAsync(item => (int?)item.OrderAddress, cancellationToken) ?? 0;
    //    return maxOrder;
    //}



    //public void AddToCompanyAddressContext(CompanyAddress companyAddress, CancellationToken cancellationToken)
    //{
    //    dbContext.CompanyAddresses.Add(companyAddress);
    //}
}