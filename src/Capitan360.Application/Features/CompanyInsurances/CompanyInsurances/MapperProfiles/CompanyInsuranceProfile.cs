using AutoMapper;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Domain.Entities.CompanyInsurances;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.MapperProfiles;

public class CompanyInsuranceProfile : Profile
{
    public CompanyInsuranceProfile()
    {
        CreateMap<CompanyInsurance, CompanyInsuranceDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null));

        CreateMap<CompanyInsuranceDto, CompanyInsurance>()
            .ForMember(dest => dest.Company, opt => opt.Ignore()) 
            .ForMember(dest => dest.CompanyInsuranceCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateCompanyInsuranceCommand, CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.CompanyInsuranceCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateCompanyInsuranceCommand, CompanyInsurance>()
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyInsuranceCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}