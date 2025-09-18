namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.Commands.Create;

public record CreateCompanyDomesticPathCommand(
    int CompanyId,
    int Active,
    string Description,
    string DescriptionForSearch,
    long? EntranceFee,
    decimal? EntranceFeeWeight,
    int EntranceFeeType,
    int SourceCountryId,
    int SourceProvinceId,
    int SourceCityId,
    int DestinationCountryId,
    int DestinationProvinceId,
    int DestinationCityId);