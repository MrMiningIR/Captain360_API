namespace Capitan360.Application.Features.PackageTypes.Commands.Update;

public record UpdatePackageTypeCommand(
    string Name,
    string Description,
    bool Active)
{
    public int Id { get; set; }
};