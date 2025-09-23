using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;

public class CreateCompanyBankCommandValidator : AbstractValidator<CreateCompanyBankCommand>
{
    public CreateCompanyBankCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد بانک الزامی است")
            .MaximumLength(50).WithMessage("کد بانک نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(2).WithMessage("کد بانک نمی‌تواند کمتر از 2 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام بانک الزامی است")
            .MaximumLength(50).WithMessage("نام بانک نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(2).WithMessage("نام بانک نمی‌تواند کمتر از 2 کاراکتر باشد");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات بانک الزامی است")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}
