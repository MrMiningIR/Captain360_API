using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;

public class CreateCompanyCommissionsCommandValidator : AbstractValidator<CreateCompanyCommissionsCommand>
{
    public CreateCompanyCommissionsCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.CommissionFromCaptainCargoWebSite)
            .GreaterThan(0).WithMessage("کمیسیون صدور در وب سایت جستجوی کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCompanyWebSite)
            .GreaterThan(0).WithMessage("کمیسیون صدور در وب سایت شرکت باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebService)
            .GreaterThan(0).WithMessage("کمیسیون صدور در وب سرویس کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCompanyWebService)
            .GreaterThan(0).WithMessage("کمیسیون صدور در وب سرویس شرکت باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoPanel)
            .GreaterThan(0).WithMessage("کمیسیون صدور در پنل کاربری کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoDesktop)
            .GreaterThan(0).WithMessage("کمیسیون صدور در از طریق نرم افزار کاپیتان کارگو باید مشخص باشد");
    }
}