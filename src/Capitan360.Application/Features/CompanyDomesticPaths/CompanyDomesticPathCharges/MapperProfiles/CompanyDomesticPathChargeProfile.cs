using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.MapperProfiles;

public class CompanyDomesticPathChargeProfile : Profile
{
    public CompanyDomesticPathChargeProfile()
    {
        CreateMap<CreateCompanyDomesticPathChargeItemCommand, CompanyDomesticPathCharge>();
        CreateMap<CompanyDomesticPathChargeContentType, CompanyDomesticPathChargeContentTypeDto>();

        CreateMap<CompanyDomesticPathCharge, CompanyDomesticPathChargeDto>()
                 .ForMember(dest => dest.CompanyDomesticPathChargeContentTypesDtos,
                     opt => opt.MapFrom(src =>
                         src.CompanyDomesticPathChargeContentTypes ?? new List<CompanyDomesticPathChargeContentType>()));


        CreateMap<UpdateCompanyDomesticPathChargeItemCommand, CompanyDomesticPathCharge>();
        CreateMap<UpdateCompanyDomesticPathContentItemCommand, CompanyDomesticPathChargeContentType>();
    }
}