using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Addresses;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyAddress : BaseEntity
{
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public bool Active { get; set; }

    public int OrderAddress { get; set; }

   
}