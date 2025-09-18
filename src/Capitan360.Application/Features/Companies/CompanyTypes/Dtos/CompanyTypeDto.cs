namespace Capitan360.Application.Features.Companies.CompanyTypes.Dtos;

public class CompanyTypeDto
{
    public int Id { get; set; }
    public string TypeName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Description { get; set; } = default!;
}