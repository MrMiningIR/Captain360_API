namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Update;

public record UpdateAddressCommand(
    int MunicipalAreaId,
    decimal Latitude,
    decimal Longitude,
    string AddressLine,
    string Mobile,
    string Tel1,
    string Tel2,
    string Zipcode,
    string Description,
    bool Active)
{
    public int Id { get; set; }
};