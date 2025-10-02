using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByNoReceiverCompanyFromDesktop;

public class GetCompanyDomesticWaybillByNoReceiverCompanyFromDesktopQueryValidator : AbstractValidator<GetCompanyDomesticWaybillByNoReceiverCompanyFromDesktopQuery>
{
    public GetCompanyDomesticWaybillByNoReceiverCompanyFromDesktopQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}
