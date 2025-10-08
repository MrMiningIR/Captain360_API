using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Create;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Identities.Permissions.Commands.Create;
using Capitan360.Application.Features.Identities.Permissions.Commands.Update;
using Capitan360.Application.Features.Identities.Permissions.Dtos;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Features.Identities.Permissions.MapperProfiles;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(d => d.Parent, opt => opt.MapFrom(s => s.ParentName));
        
        CreateMap<CreatePermissionCommand, Permission>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.RolePermissions, opt => opt.Ignore())
            .ForMember(d => d.UserPermissions, opt => opt.Ignore());

        CreateMap<UpdatePermissionCommand, Permission>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.RolePermissions, opt => opt.Ignore())
            .ForMember(d => d.UserPermissions, opt => opt.Ignore());
    }
}



