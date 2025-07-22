using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;

public class CreateCompanyCommissionsValidator : AbstractValidator<CreateCompanyCommissionsCommand>
{
    public CreateCompanyCommissionsValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.CommissionFromCaptainCargoWebSite)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سایت کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد");

        RuleFor(x => x.CommissionFromCompanyWebSite)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سایت شرکت باید بین 0 و 9999999999999999.99 باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebService)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سرویس کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد");

        RuleFor(x => x.CommissionFromCompanyWebService)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سرویس شرکت باید بین 0 و 9999999999999999.99 باشد");

        RuleFor(x => x.CommissionFromCaptainCargoPanel)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون پنل کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد");

        RuleFor(x => x.CommissionFromCaptainCargoDesktop)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون دسکتاپ کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد");
    }
}