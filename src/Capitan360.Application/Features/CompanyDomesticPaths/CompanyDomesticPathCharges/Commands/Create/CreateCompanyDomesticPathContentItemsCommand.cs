namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Create;

public record CreateCompanyDomesticPathContentItemsCommand
{
    public List<CreateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}