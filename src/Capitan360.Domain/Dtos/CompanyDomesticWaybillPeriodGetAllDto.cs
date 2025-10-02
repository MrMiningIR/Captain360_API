namespace Capitan360.Domain.Dtos;

public class CompanyDomesticWaybillPeriodGetAllDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = default!;
    public string Code { get; set; } = default!;
    public long StartNumber { get; set; }
    public long EndNumber { get; set; }
    public bool Active { get; set; }
    public string Description { get; set; } = default!;
    public long CountReady { get; set; }
}
