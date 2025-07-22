using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.AddressRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Capitan360.Infrastructure.Repositories.AddressImpl;

public class CompanyAddressRepository(ApplicationDbContext dbContext) : ICompanyAddressRepository
{
    //public async Task<CompanyAddress?> GetByIdAsync(int addressId, CancellationToken cancellationToken)
    //{
    //    return await dbContext.CompanyAddresses.FirstOrDefaultAsync(ad => ad.AddressId == addressId, cancellationToken);

    //}

    //public async Task<int> OrderAddress(int commandCompanyId, CancellationToken cancellationToken)
    //{
    //    //return await dbContext.CompanyAddresses
    //    //    .Where(ca => ca.CompanyId == commandCompanyId)
    //    //    .OrderByDescending(ca => ca.OrderAddress)
    //    //    .Select(ca => ca.OrderAddress)
    //    //    .FirstOrDefaultAsync(cancellationToken);


    //    var maxOrder = await dbContext.CompanyAddresses
    //        .Where(ca => ca.CompanyId == commandCompanyId)
    //        .MaxAsync(ca => (int?)ca.OrderAddress, cancellationToken) ?? 0;
    //    return maxOrder;
    //}

   

    //public void AddToCompanyAddressContext(CompanyAddress companyAddress, CancellationToken cancellationToken)
    //{
    //    dbContext.CompanyAddresses.Add(companyAddress);
    //}
}