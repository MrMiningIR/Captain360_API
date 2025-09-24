using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;

public class CreateCompanyBankCommandValidator : AbstractValidator<CreateCompanyBankCommand>
{
    public CreateCompanyBankCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد الزامی است")
            .MaximumLength(10).WithMessage("کد نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است")
            .MaximumLength(30).WithMessage("نام نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}