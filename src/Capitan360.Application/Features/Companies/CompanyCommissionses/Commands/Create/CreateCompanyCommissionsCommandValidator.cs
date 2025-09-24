using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;

public class CreateCompanyCommissionsCommandValidator : AbstractValidator<CreateCompanyCommissionsCommand>
{
    public CreateCompanyCommissionsCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.CommissionFromCaptainCargoWebSite)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سایت جستجوی کاپیتان 360 باید بزرگتر یا برابر صفر باشد");

        RuleFor(x => x.CommissionFromCompanyWebSite)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سایت شرکت باید بزرگتر یا برابر صفر باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebService)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سرویس کاپیتان 360 باید بزرگتر یا برابر صفر باشد");

        RuleFor(x => x.CommissionFromCompanyWebService)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سرویس شرکت باید بزرگتر یا برابر صفر باشد");

        RuleFor(x => x.CommissionFromCaptainCargoPanel)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در پنل کاربری کاپیتان 360 باید بزرگتر یا برابر صفر باشد");

        RuleFor(x => x.CommissionFromCaptainCargoDesktop)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در از طریق نرم افزار کاپیتان کارگو باید بزرگتر یا برابر صفر باشد");
    }
}