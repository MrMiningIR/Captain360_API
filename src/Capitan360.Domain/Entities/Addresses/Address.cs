using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Addresses;

public class Address : BaseEntity
{
    [ForeignKey(nameof(AddressType))]
    public int AddressTypeId { get; set; }

    [ForeignKey(nameof(Company))]
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }
    public Area? Country { get; set; }

    [ForeignKey(nameof(Province))]
    public int ProvinceId { get; set; }
    public Area? Province { get; set; }

    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public Area? City { get; set; }

    [ForeignKey(nameof(MunicipalArea))]
    public int MunicipalAreaId { get; set; }
    public Area? MunicipalArea { get; set; }

    public string AddressLine { get; set; } = default!;

    public string Mobile { get; set; } = default!;

    public string Tel1 { get; set; } = default!;

    public string Tel2 { get; set; } = default!;

    public string Zipcode { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool Active { get; set; }

    public int Order { get; set; }
}