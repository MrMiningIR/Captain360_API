using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetByNoSenderCompanyFromDesktop;

public class GetCompanyManifestFormByNoSenderCompanyFromDesktopQueryValidator : AbstractValidator<GetCompanyManifestFormByNoSenderCompanyFromDesktopQuery>
{
    public GetCompanyManifestFormByNoSenderCompanyFromDesktopQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}
