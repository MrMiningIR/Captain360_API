using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.MapperProfiles;

public class CompanyDomesticPathChargeProfile : Profile
{
    public CompanyDomesticPathChargeProfile()
    {
        CreateMap<CreateCompanyDomesticPathChargeItemCommand, Domain.Entities.Companies.CompanyDomesticPathCharge>();
        CreateMap<Domain.Entities.Companies.CompanyDomesticPathChargeContentType, CompanyDomesticPathChargeContentTypeDto>();

        CreateMap<Domain.Entities.Companies.CompanyDomesticPathCharge, CompanyDomesticPathChargeDto>()
                 .ForMember(dest => dest.CompanyDomesticPathChargeContentTypesDtos,
                     opt => opt.MapFrom(src =>
                         src.CompanyDomesticPathChargeContentTypes ?? new List<Domain.Entities.Companies.CompanyDomesticPathChargeContentType>()));


        CreateMap<UpdateCompanyDomesticPathChargeItemCommand, Domain.Entities.Companies.CompanyDomesticPathCharge>();
        CreateMap<UpdateCompanyDomesticPathContentItemCommand, Domain.Entities.Companies.CompanyDomesticPathChargeContentType>();
    }
}