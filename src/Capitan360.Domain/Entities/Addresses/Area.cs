using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.CompanyManifestForms;

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

    public ICollection<CompanyManifestForm> CompanyManifestFormSourceCountries { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormSourceProvinces { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormSourceCities { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormDestinationCountries { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormDestinationProvinces { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormDestinationCities { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillSourceCountries { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillSourceProvinces { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillSourceCities { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillSourceMunicipalAreas { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillDestinationCountries { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillDestinationProvinces { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillDestinationCities { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillDestinationMunicipalAreas { get; set; } = [];
}
