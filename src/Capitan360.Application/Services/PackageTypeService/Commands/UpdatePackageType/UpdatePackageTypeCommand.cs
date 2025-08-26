namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

public record UpdatePackageTypeCommand(
    string PackageTypeName,
    string? PackageTypeDescription,
    bool PackageTypeActive
)
{
    public int Id { get; set; }
};