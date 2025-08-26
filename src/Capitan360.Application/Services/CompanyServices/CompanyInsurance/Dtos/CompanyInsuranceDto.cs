namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

public class CompanyInsuranceDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? CaptainCargoCode { get; set; }
    public decimal Tax { get; set; }
    public long Scale { get; set; }
    public bool Active { get; set; }
    public string? Description { get; set; }
}