namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Create;

public record CreateCompanyDomesticPathContentItemsCommand
{
    public List<CreateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}