namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Update;

public record UpdateCompanyDomesticPathContentItemsCommand
{
    public List<UpdateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}