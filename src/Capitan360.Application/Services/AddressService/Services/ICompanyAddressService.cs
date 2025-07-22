using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Application.Services.AddressService.Services;

public interface ICompanyAddressService
{
    Task<int> CreateCompanyAddressAsync(CompanyAddress companyAddress, CancellationToken cancellationToken);
}