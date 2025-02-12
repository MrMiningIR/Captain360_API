using Capitan360.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Entities.CompanyEntity;
using NetTopologySuite.Geometries;

namespace Capitan360.Domain.Entities.AddressEntity;

public class Address : Entity
{

    public int CountryId { get; set; }

    public int ProvinceId { get; set; }

    public int CityId { get; set; }


    public string AddressLine { get; set; } = default!;


    public string? Mobile { get; set; }


    public string? Tel1 { get; set; }


    public string? Tel2 { get; set; }


    public string? Zipcode { get; set; }


    public string? Description { get; set; }


    public Point? Coordinates { get; set; }


    public ICollection<CompanyAddress> CompanyAddresses { get; set; } = [];


    // Navigation Properties

    public Area Country { get; set; } = null!;
    public Area Province { get; set; } = null!;
    public Area City { get; set; } = null!;

}