using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;

public class UpdateCompanyCommissionsCommandValidator : AbstractValidator<UpdateCompanyCommissionsCommand>
{
    public UpdateCompanyCommissionsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه کمیسیون باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebSite)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سایت کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCaptainCargoWebSite.HasValue);

        RuleFor(x => x.CommissionFromCompanyWebSite)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سایت شرکت باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCompanyWebSite.HasValue);

        RuleFor(x => x.CommissionFromCaptainCargoWebService)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سرویس کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCaptainCargoWebService.HasValue);

        RuleFor(x => x.CommissionFromCompanyWebService)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون وب‌سرویس شرکت باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCompanyWebService.HasValue);

        RuleFor(x => x.CommissionFromCaptainCargoPanel)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون پنل کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCaptainCargoPanel.HasValue);

        RuleFor(x => x.CommissionFromCaptainCargoDesktop)
            .InclusiveBetween(0m, 9999999999999999.99m).WithMessage("کمیسیون دسکتاپ کاپیتان کارگو باید بین 0 و 9999999999999999.99 باشد")
            .When(x => x.CommissionFromCaptainCargoDesktop.HasValue);
    }
}