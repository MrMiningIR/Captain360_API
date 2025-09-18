using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyUri.MapperProfiles;

public class CompanyUriProfile : Profile
{
    public CompanyUriProfile()
    {
        CreateMap<CreateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();
        CreateMap<UpdateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();

        CreateMap<Domain.Entities.Companies.CompanyUri, CompanyUriDto>();
    }
}