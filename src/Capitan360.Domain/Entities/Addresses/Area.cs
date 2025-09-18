using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Domain.Entities.ManifestForms;

namespace Capitan360.Domain.Entities.Addresses;

public class Area : BaseEntity
{
    [ForeignKey(nameof(Parent))]
    public int? ParentId { get; set; }
    public Area? Parent { get; set; }

    public short LevelId { get; set; }

    public string PersianName { get; set; } = default!;

    public string EnglishName { get; set; } = default!;

    public string Code { get; set; } = default!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public ICollection<Area> Children { get; set; } = [];

    public ICollection<Address> AddressCountries { get; set; } = [];

    public ICollection<Address> AddressProvinces { get; set; } = [];

    public ICollection<Address> AddressCities { get; set; } = [];

    public ICollection<Address> AddressMunicipalAreas { get; set; } = [];

    public ICollection<Company> CompanyCountries { get; set; } = [];

    public ICollection<Company> CompanyProvinces { get; set; } = [];

    public ICollection<Company> CompanyCities { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathSourceCountries { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathSourceProvinces { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathSourceCities { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathDestinationCountries { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathDestinationProvinces { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPathDestinationCities { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormSourceCountries { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormSourceProvinces { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormSourceCities { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormDestinationCountries { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormDestinationProvinces { get; set; } = [];

    public ICollection<ManifestForm> ManifestFormDestinationCities { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillSourceCountries { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillSourceProvinces { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillSourceCities { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillSourceMunicipalAreas { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillDestinationCountries { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillDestinationProvinces { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillDestinationCities { get; set; } = [];

    public ICollection<DomesticWaybill> DomesticWaybillDestinationMunicipalAreas { get; set; } = [];
}
