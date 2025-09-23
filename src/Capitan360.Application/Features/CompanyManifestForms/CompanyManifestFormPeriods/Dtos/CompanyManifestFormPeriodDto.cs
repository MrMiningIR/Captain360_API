namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;

public class CompanyManifestFormPeriodDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string Code { get; set; } = default!;
    public long StartNumber { get; set; }
    public long EndNumber { get; set; }
    public bool Active { get; set; }
    public string Description { get; set; } = default!;
}
