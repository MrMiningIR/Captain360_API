using AutoMapper;
using Capitan360.Application.Services.AddressService.Commands.AddNewAddressToCompany;
using Capitan360.Application.Services.AddressService.Commands.CreateAddress;
using Capitan360.Application.Services.AddressService.Commands.UpdateAddress;
using Capitan360.Application.Services.AddressService.Commands.UpdateCompanyAddress;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;
using Capitan360.Domain.Entities.AddressEntity;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Application.Services.AddressService.MapperProfiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<UpdateAddressCommand, Address>()
            //.ForMember(dest => dest.CountryId, opt => opt.Condition(src => src.CountryId.HasValue))
            //.ForMember(dest => dest.ProvinceId, opt => opt.Condition(src => src.ProvinceId.HasValue))
            //.ForMember(dest => dest.CityId, opt => opt.Condition(src => src.CityId.HasValue))
            .ForMember(dest => dest.AddressLine, opt => opt.Condition(src => src.AddressLine != null))
            .ForMember(dest => dest.Mobile, opt => opt.Condition(src => src.Mobile != null))
            .ForMember(dest => dest.Tel1, opt => opt.Condition(src => src.Tel1 != null))
            .ForMember(dest => dest.Tel2, opt => opt.Condition(src => src.Tel2 != null))
            .ForMember(dest => dest.Zipcode, opt => opt.Condition(src => src.Zipcode != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.Latitude, opt => opt.Condition(src => !src.Latitude.Equals(0)))
            .ForMember(dest => dest.Longitude, opt => opt.Condition(src => !src.Latitude.Equals(0)));
        //.ForMember(dest => dest.AddressType, opt => opt.Condition(src => src.AddressType.HasValue))
        // .ForMember(dest => dest.Coordinates, opt => opt.Condition(src => src.Coordinates != null))
        // .ForMember(dest => dest.Latitude, opt => opt.Ignore());
        CreateMap<Address, AddressDto>();
        //.ForMember(dest => dest.CompanyAddressId,
        //    opt => opt.MapFrom(src => src.CompanyAddresses.FirstOrDefault().Id));
        ;
        //.ForMember(dest => dest.Coordinates, opt => opt.Ignore());

        CreateMap<UpdateCompanyAddressCommand, Address>()
            //.ForMember(dest => dest.CountryId, opt => opt.Condition(src => src.CountryId.HasValue))
            //.ForMember(dest => dest.ProvinceId, opt => opt.Condition(src => src.ProvinceId.HasValue))
            //.ForMember(dest => dest.CityId, opt => opt.Condition(src => src.CityId.HasValue))
            .ForMember(dest => dest.AddressLine, opt => opt.Condition(src => src.AddressLine != null))
            .ForMember(dest => dest.Mobile, opt => opt.Condition(src => src.Mobile != null))
            .ForMember(dest => dest.Tel1, opt => opt.Condition(src => src.Tel1 != null))
            .ForMember(dest => dest.Tel2, opt => opt.Condition(src => src.Tel2 != null))
            .ForMember(dest => dest.Zipcode, opt => opt.Condition(src => src.Zipcode != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.AddressType, opt => opt.Condition(src => src.AddressType.HasValue))
            .ForMember(dest => dest.Latitude, opt => opt.Condition(src => !src.Latitude.Equals(0)))
            .ForMember(dest => dest.Longitude, opt => opt.Condition(src => !src.Latitude.Equals(0)))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

        ;

        CreateMap<AddNewAddressToCompanyCommand, Address>()
            .ForMember(dest => dest.AddressLine, opt => opt.MapFrom(src => src.AddressLine))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.Tel1, opt => opt.MapFrom(src => src.Tel1))
            .ForMember(dest => dest.Tel2, opt => opt.MapFrom(src => src.Tel2))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
        //.ForMember(dest => dest.AddressType, opt => opt.MapFrom(src => src.AddressType))

        CreateMap<CreateCompanyCommand, Company>();

        CreateMap<CompanyAddressMappingModel, CompanyAddress>();
    }
}