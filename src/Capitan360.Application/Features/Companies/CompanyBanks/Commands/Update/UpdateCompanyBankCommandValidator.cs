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
