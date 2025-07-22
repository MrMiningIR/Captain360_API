namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

public record UpdatePackageTypeCommand(
    int CompanyTypeId,
    string PackageTypeName,
    string PackageTypeDescription,
    bool Active
)
{
    public int Id { get; set; }
};