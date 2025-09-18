namespace Capitan360.Application.Features.PackageTypeService.Commands.Update;

public record UpdatePackageTypeCommand(
    string PackageTypeName,
    string? PackageTypeDescription,
    bool PackageTypeActive
)
{
    public int Id { get; set; }
};