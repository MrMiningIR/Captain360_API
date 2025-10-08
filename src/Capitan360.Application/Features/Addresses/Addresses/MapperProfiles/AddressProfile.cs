using AutoMapper;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Create;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Domain.Entities.Addresses;

namespace Capitan360.Application.Features.Addresses.Addresses.MapperProfiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>()
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company != null ? s.Company.Name : null))
            .ForMember(d => d.UserNameFamily, opt => opt.MapFrom(s => s.User != null ? s.User.NameFamily : null))
            .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.Country != null ? s.Country.PersianName : null))
            .ForMember(d => d.ProvinceName, opt => opt.MapFrom(s => s.Province != null ? s.Province.PersianName : null))
            .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City != null ? s.City.PersianName : null))
            .ForMember(d => d.MunicipalAreaName, opt => opt.MapFrom(s => s.MunicipalArea != null ? s.MunicipalArea.PersianName : null));

        CreateMap<CreateAddressCommand, Address>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateAddressCommand, Address>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanyId, opt => opt.Ignore())
            .ForMember(d => d.UserId, opt => opt.Ignore())
            .ForMember(d => d.CountryId, opt => opt.Ignore())
            .ForMember(d => d.ProvinceId, opt => opt.Ignore())
            .ForMember(d => d.CityId, opt => opt.Ignore())
            .ForMember(d => d.Order, opt => opt.Ignore());
    }
}