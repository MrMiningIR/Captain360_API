using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;

public class UpdateCompanyBankCommandValidator : AbstractValidator<UpdateCompanyBankCommand>
{
    public UpdateCompanyBankCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک الزامی است");

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
