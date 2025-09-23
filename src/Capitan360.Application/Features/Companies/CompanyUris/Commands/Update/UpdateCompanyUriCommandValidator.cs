using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;

public class UpdateCompanyUriCommandValidator : AbstractValidator<UpdateCompanyUriCommand>
{
    public UpdateCompanyUriCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس URI الزامی است");

        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("آدرس URI الزامی است")
            .MaximumLength(100).WithMessage("آدرس URI نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .MinimumLength(4).WithMessage("آدرس URI نمی‌تواند کمتر از 4 کاراکتر باشد");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات الزامی است.")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}