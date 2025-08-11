namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;

public class CompanyUriDto
{
    public int Id { get; set; }
    public string Uri { get; set; } = default!;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public int CompanyId { get; set; }
}