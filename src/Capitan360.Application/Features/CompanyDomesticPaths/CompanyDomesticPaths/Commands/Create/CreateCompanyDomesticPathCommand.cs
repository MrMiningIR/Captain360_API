namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;

public record CreateCompanyDomesticPathCommand(
    int CompanyId,
    int Active,
    string Description,
    string DescriptionForSearch,

    int SourceCountryId,
    int SourceProvinceId,
    int SourceCityId,
    int DestinationCountryId,
    int DestinationProvinceId,
    int DestinationCityId);