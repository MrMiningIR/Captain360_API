using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Services;
using Capitan360.Application.Services.CompanyContentTypeService.Services;
using Capitan360.Application.Services.CompanyPackageTypeService.Services;
using Capitan360.Application.Services.CompanyServices.Company.Services;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Services;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Services;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Services;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Services;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Services;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;
using Capitan360.Application.Services.CompanyServices.CompanyType.Services;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Services;
using Capitan360.Application.Services.ContentTypeService.Services;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Application.Services.PackageTypeService.Services;
using Capitan360.Application.Services.Permission.Services;
using Capitan360.Application.Services.Role;
using Capitan360.Application.Services.UserPermission.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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