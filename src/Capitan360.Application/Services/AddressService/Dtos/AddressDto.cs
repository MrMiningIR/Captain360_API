using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.AddressService.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public string AddressLine { get; set; } = default!;
    public string? Mobile { get; set; }
    public string? Tel1 { get; set; }
    public string? Tel2 { get; set; }
    public string? Zipcode { get; set; }
    public string? Description { get; set; }

    public int OrderAddress { get; set; }
    public bool Active { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public AddressType AddressType { get; set; }
    // public Point? Coordinates { get; set; }
}