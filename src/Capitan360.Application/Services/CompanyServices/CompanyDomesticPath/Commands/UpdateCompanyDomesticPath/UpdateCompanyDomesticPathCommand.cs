using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;

public record UpdateCompanyDomesticPathCommand(
    int CompanyId,
    bool Active,
    string Description,
    string DescriptionForSearch,
    long EntranceFee,
    decimal EntranceWeight,
    int EntranceType,
    int SourceCountryId,
    int SourceProvinceId,
    int SourceCityId,
    int DestinationCountryId,
    int DestinationProvinceId,
    int DestinationCityId)
{
    public int Id { get; set; }
}