using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Services;
using Capitan360.Application.Features.Addresses.Areas.Services;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Services;
using Capitan360.Application.Features.Companies.CompanyInsurances.Services;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Services;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;
using Capitan360.Application.Features.Companies.Companies.Services;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Services;
using Capitan360.Application.Features.Companies.CompanyDomesticPaths.Services;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Services;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Services;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Services;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Services;
using Capitan360.Application.Features.Companies.CompanyType.Services;
using Capitan360.Application.Features.Companies.CompanyUri.Services;
using Capitan360.Application.Features.ContentTypeService.Services;
using Capitan360.Application.Features.PackageTypeService.Services;
using Capitan360.Application.Features.Permission.Services;
using Capitan360.Application.Features.Role;
using Capitan360.Application.Features.UserPermission.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Capitan360.Application.Features.Identities.Identities.Services;

namespace Capitan360.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        service.AddScoped<IIdentityService, IdentityService>();
        service.AddScoped<ICompanyService, CompanyService>();
        service.AddScoped<IAddressService, AddressService>();
        service.AddScoped<ICompanyUriService, CompanyUriService>();
        service.AddScoped<ICompanySmsPatternsService, CompanySmsPatternsService>();
        service.AddScoped<ICompanyPreferencesService, CompanyPreferencesService>();
        service.AddScoped<ICompanyCommissionsService, CompanyCommissionsService>();
        service.AddScoped<ICompanyTypeService, CompanyTypeService>();
        service.AddScoped<IPermissionService, PermissionService>();
        service.AddScoped<IAreaService, AreaService>();
        service.AddScoped<IContentTypeService, ContentTypeService>();
        service.AddScoped<IPackageTypeService, PackageTypeService>();
        service.AddScoped<ICompanyDomesticPathsService, CompanyDomesticPathsService>();
        service.AddScoped<ICompanyDomesticPathStructPricesService, CompanyDomesticPathStructPricesService>();
        service.AddScoped<ICompanyDomesticPathChargeService, CompanyDomesticPathChargeService>();
        service.AddScoped<ICompanyDomesticPathChargeContentTypeService, CompanyDomesticPathChargeContentTypeService>();
        service.AddScoped<ICompanyInsuranceService, CompanyInsuranceService>();
        service.AddScoped<ICompanyInsuranceChargeService, CompanyInsuranceChargeService>();
        service.AddScoped<ICompanyContentTypeService, CompanyContentTypeService>();
        service.AddScoped<ICompanyPackageTypeService, CompanyPackageTypeService>();
        service.AddScoped<PermissionCollectorService>();
        service.AddScoped<IUserPermissionService, UserPermissionService>();
        service.AddScoped<IRoleService, RoleService>();

        service.AddHttpContextAccessor();

        service.AddScoped<IUserContext, UserContext>();

        service.AddAutoMapper(applicationAssembly);

        service.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        service.AddControllers()

            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .SelectMany(entry => entry.Value?.Errors.Select(error => new CustomValidationError
                        {
                            PropertyName = entry.Key, // نام پراپرتی
                            ErrorMessage = error.ErrorMessage,
                            ErrorCode = error.Exception?.GetType().Name ?? "VALIDATION_ERROR"
                        }) ?? Array.Empty<CustomValidationError>())
                        .ToList();

                    var response = new ApiResponse<List<CustomValidationError>>(
                        statusCode: 400,
                        message: "Validation failed",
                        data: errors
                    );

                    return new BadRequestObjectResult(response);
                };
            });
    }
}