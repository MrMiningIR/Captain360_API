namespace Capitan360.Application.Features.Addresses.Areas.Commands.Update;

public record UpdateAreaCommand(
    string PersianName,
    string EnglishName,
    string Code,
    decimal Latitude,
    decimal Longitude)
{
    public int Id { get; set; }
};