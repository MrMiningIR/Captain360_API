using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyAddress : Entity
{
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public bool Active { get; set; }

    public int OrderAddress { get; set; }

    public string CaptainCargoName { get; set; } = default!;
    public string CaptainCargoCode { get; set; } = default!;
}