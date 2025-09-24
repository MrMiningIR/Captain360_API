using System;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;

public class CreateCompanyUriValidator : AbstractValidator<CreateCompanyUriCommand>
{
    public CreateCompanyUriValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("آدرس URI الزامی است")
            .MaximumLength(100).WithMessage("آدرس URI نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}
