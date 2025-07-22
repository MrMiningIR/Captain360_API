using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;

public class CreateCompanyUriValidator : AbstractValidator<CreateCompanyUriCommand>
{
    public CreateCompanyUriValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("آدرس URI الزامی است")
            .MaximumLength(100).WithMessage("آدرس URI نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .MinimumLength(5).WithMessage("آدرس URI نمی‌تواند کمتر از 5 کاراکتر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("توضیحات نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}