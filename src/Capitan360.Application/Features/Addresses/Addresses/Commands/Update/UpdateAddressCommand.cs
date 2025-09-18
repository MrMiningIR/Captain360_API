namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Update;

public record UpdateAddressCommand(

    string AddressLine,
    string? Mobile,
    string? Tel1,
    string? Tel2,
    string? Zipcode,
    string? Description,
   double? Latitude,
    double? Longitude,
    bool Active

)
{
    public int Id { get; set; }
};