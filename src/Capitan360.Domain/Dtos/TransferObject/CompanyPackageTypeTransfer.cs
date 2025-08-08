namespace Capitan360.Domain.Dtos.TransferObject;

public class CompanyPackageTypeTransfer
{
    public int Id { get; set; }

    public string PackageTypeName { get; set; } = default!;
    public bool Active { get; set; }

    public int OrderPackageType { get; set; }
    public string? CompanyPackageTypeDescription { get; set; }
}