namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Update;

public record UpdateCompanyDomesticPathContentItemsCommand
{
    public List<UpdateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}