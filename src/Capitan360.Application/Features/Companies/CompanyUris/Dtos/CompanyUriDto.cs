namespace Capitan360.Application.Features.Companies.CompanyUri.Dtos;

public class CompanyUriDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; } 
    public string Uri { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Active { get; set; }
    public bool Captain360Uri { get; set; }
}