using AutoMapper;
using Capitan360.Application.Services.AddressService.Commands.CreateArea;
using Capitan360.Application.Services.AddressService.Commands.UpdateArea;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Application.Services.AddressService.MapperProfiles;

public class AreaProfile : Profile
{
    public AreaProfile()
    {
        CreateMap<CreateAreaCommand, Area>();
        CreateMap<Area, AreaDto>();
        CreateMap<Area, CityAreaDto>();
        CreateMap<Area, ProvinceAreaDto>();
        CreateMap<Area, AreaItemDto>();
        CreateMap<UpdateAreaCommand, Area>()
            .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentId.HasValue))
            .ForMember(dest => dest.LevelId, opt => opt.Condition(src => src.LevelId.HasValue))
            .ForMember(dest => dest.PersianName, opt => opt.Condition(src => src.PersianName != null))
            .ForMember(dest => dest.EnglishName, opt => opt.Condition(src => src.EnglishName != null))
            .ForMember(dest => dest.Code, opt => opt.Condition(src => src.Code != null))
           .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());
        //.ForMember(dest => dest.Coordinates, opt => opt.Condition(src => src.Coordinates != null))
    }
}