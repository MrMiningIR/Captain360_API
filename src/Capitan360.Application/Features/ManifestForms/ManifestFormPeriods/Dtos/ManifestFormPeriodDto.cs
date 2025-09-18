namespace Capitan360.Application.Features.ManifestForms.ManifestFormPeriods.Dtos;

public class ManifestFormPeriodDto
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
