namespace Capitan360.Application.Services.CompanyServices.CompanyType.Dtos;

public class CompanyTypeDto
{
    public int Id { get; set; }
    public string TypeName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string? Description { get; set; }
}