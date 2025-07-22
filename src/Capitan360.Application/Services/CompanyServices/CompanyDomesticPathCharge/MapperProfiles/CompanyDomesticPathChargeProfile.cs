using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.MapperProfiles;

public class CompanyDomesticPathChargeProfile : Profile
{
    public CompanyDomesticPathChargeProfile()
    {
        CreateMap<CreateCompanyDomesticPathChargeItemCommand, Domain.Entities.CompanyEntity.CompanyDomesticPathCharge>();
        CreateMap<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType, CompanyDomesticPathChargeContentTypeDto>();

        CreateMap<Domain.Entities.CompanyEntity.CompanyDomesticPathCharge, CompanyDomesticPathChargeDto>()
                 .ForMember(dest => dest.CompanyDomesticPathChargeContentTypesDtos,
                     opt => opt.MapFrom(src =>
                         src.CompanyDomesticPathChargeContentTypes ?? new List<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>()));


        CreateMap<UpdateCompanyDomesticPathChargeItemCommand, Domain.Entities.CompanyEntity.CompanyDomesticPathCharge>();
        CreateMap<UpdateCompanyDomesticPathContentItemCommand, Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>();
    }
}