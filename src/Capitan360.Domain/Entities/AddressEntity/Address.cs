using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Entities.AddressEntity;

public class Address : Entity
{
    public int CompanyId { get; set; } 

    public string AddressLine { get; set; } = default!;


    public string? Mobile { get; set; }


    public string? Tel1 { get; set; }


    public string? Tel2 { get; set; }


    public string? Zipcode { get; set; }


    public string? Description { get; set; }

    public AddressType AddressType { get; set; }


    //public Point? Coordinates { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool Active { get; set; }

    public int OrderAddress { get; set; }

    public Company Company { get; set; }

    //  public ICollection<CompanyAddress> CompanyAddresses { get; set; } = [];





}