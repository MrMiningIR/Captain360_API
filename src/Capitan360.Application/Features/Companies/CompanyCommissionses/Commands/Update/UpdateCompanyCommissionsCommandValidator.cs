using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;

public class UpdateCompanyCommissionsCommandValidator : AbstractValidator<UpdateCompanyCommissionsCommand>
{
    public UpdateCompanyCommissionsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه کمیسیون باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebSite)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سایت جستجوی کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCompanyWebSite)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سایت شرکت باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoWebService)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سرویس کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCompanyWebService)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در وب سرویس شرکت باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoPanel)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در پنل کاربری کاپیتان 360 باید مشخص باشد");

        RuleFor(x => x.CommissionFromCaptainCargoDesktop)
            .GreaterThanOrEqualTo(0).WithMessage("کمیسیون صدور در از طریق نرم افزار کاپیتان کارگو باید مشخص باشد");
    }
}