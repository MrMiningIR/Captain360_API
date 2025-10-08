using AutoMapper;
using Capitan360.Application.Features.Addresses.Areas.Commands.Create;
using Capitan360.Application.Features.Addresses.Areas.Commands.Update;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Domain.Entities.Addresses;

namespace Capitan360.Application.Features.Addresses.Areas.MapperProfiles;

public class AreaProfile : Profile
{
    public AreaProfile()
    {
        CreateMap<Area, AreaDto>()
            .ForMember(d => d.ParentName, o => o.MapFrom(s => s.Parent != null ? s.Parent.PersianName : null))
            .ForMember(d => d.LevelName, o => o.MapFrom<AreaLevelNameResolver>());

        CreateMap<CreateAreaCommand, Area>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Parent, o => o.Ignore())
            .ForMember(d => d.Children, o => o.Ignore())
            .ForMember(d => d.AddressCountries, o => o.Ignore())
            .ForMember(d => d.AddressProvinces, o => o.Ignore())
            .ForMember(d => d.AddressCities, o => o.Ignore())
            .ForMember(d => d.AddressMunicipalAreas, o => o.Ignore())
            .ForMember(d => d.CompanyCountries, o => o.Ignore())
            .ForMember(d => d.CompanyProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathReceiverCompanies, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceMunicipalAreas, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationMunicipalAreas, o => o.Ignore())
            ;

        CreateMap<UpdateAreaCommand, Area>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.ParentId, o => o.Ignore())
            .ForMember(d => d.Parent, o => o.Ignore())
            .ForMember(d => d.LevelId, o => o.Ignore())
            .ForMember(d => d.Children, o => o.Ignore())
            .ForMember(d => d.AddressCountries, o => o.Ignore())
            .ForMember(d => d.AddressProvinces, o => o.Ignore())
            .ForMember(d => d.AddressCities, o => o.Ignore())
            .ForMember(d => d.AddressMunicipalAreas, o => o.Ignore())
            .ForMember(d => d.CompanyCountries, o => o.Ignore())
            .ForMember(d => d.CompanyProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticPathReceiverCompanies, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyManifestFormDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillSourceMunicipalAreas, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationCountries, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationProvinces, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationCities, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillDestinationMunicipalAreas, o => o.Ignore());
    }
}



public sealed class AreaLevelNameResolver : IValueResolver<Area, AreaDto, string?>
{
    private readonly IAreaLevelNameProvider _provider;

    public AreaLevelNameResolver(IAreaLevelNameProvider provider)
        => _provider = provider;

    public string? Resolve(Area source, AreaDto destination, string? destMember, ResolutionContext context)
        => _provider.GetLevelName(source.LevelId);
}
public interface IAreaLevelNameProvider
{
    string? GetLevelName(short levelId);
}
public sealed class InMemoryAreaLevelNameProvider : IAreaLevelNameProvider
{
    private static readonly Dictionary<short, string> _map = new()
    {
        { 1, "کشور" },
        { 2, "استان" },
        { 3, "شهر" },
        { 4, "منطقه شهرداری" },
    };

    public string? GetLevelName(short levelId)
        => _map.TryGetValue(levelId, out var name) ? name : null;
}