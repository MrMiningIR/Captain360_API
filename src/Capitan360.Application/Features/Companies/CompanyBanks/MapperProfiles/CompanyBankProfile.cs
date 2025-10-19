using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyBanks.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyBanks.MapperProfiles;

public class CompanyBankProfile : Profile
{
    public CompanyBankProfile()
    {
        CreateMap<CompanyBank, CompanyBankDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null));

        CreateMap<CompanyBankDto, CompanyBank>()
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankSenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateCompanyBankCommand, CompanyBank>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.Order, opt => opt.Ignore()) 
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankSenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateCompanyBankCommand, CompanyBank>()
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankSenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyBankReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}
