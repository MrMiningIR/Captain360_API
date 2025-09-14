using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Services.AddressService.Services;

public interface ICompanyAddressService
{
    Task<int> CreateCompanyAddressAsync(CompanyAddress companyAddress, CancellationToken cancellationToken);
}