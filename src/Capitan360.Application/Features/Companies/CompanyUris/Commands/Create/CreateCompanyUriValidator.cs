using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUri.Commands.CreateCompanyUri;

public class CreateCompanyUriValidator : AbstractValidator<CreateCompanyUriCommand>
{
    public CreateCompanyUriValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("آدرس URI الزامی است")
            .MaximumLength(100).WithMessage("آدرس URI نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .MinimumLength(4).WithMessage("آدرس URI نمی‌تواند کمتر از 4 کاراکتر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}