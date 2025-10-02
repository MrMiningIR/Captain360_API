using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Issue;

public class IssueCompanyManifestFormCommandValidator : AbstractValidator<IssueCompanyManifestFormCommand>
{
    public IssueCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverUserInsertedCode)
            .Null()
            .When(x => x.CompanyReceiverId.HasValue)
            .WithMessage("در صورتی که شماره شناسیی نماینده وارد شده است کد نماینده نامعتبر است");

        RuleFor(x => x.CompanyReceiverUserInsertedCode)
                .NotNull().WithMessage("کد نماینده نمی‌تواند خالی باشد")
                .NotEmpty().WithMessage("کد نماینده را وارد کنید.")
                .MaximumLength(10).WithMessage("کد نماینده نمی‌تواند بیشتر از 10 کاراکتر باشد")
                .When(x => !x.CompanyReceiverId.HasValue);

        RuleFor(x => x)
                .Custom((instance, context) =>
                {
                    var hasId = instance.CompanyReceiverId.HasValue;
                    var codeIsSet = !string.IsNullOrWhiteSpace(instance.CompanyReceiverUserInsertedCode);

                    if (hasId)
                    {
                        if (codeIsSet)
                        {
                            context.AddFailure("در صورتی که شماره شناسایی نماینده وارد شده است کد نماینده نامعتبر است");
                        }
                    }
                    else
                    {
                        if (!codeIsSet)
                        {
                            context.AddFailure("کد/نام نماینده نمی‌تواند خالی باشد");
                        }
                        else
                        {
                            if (!codeIsSet)
                                context.AddFailure("کد نماینده نمی‌تواند خالی باشد");
                        }
                    }
                });

        RuleFor(x => x.Date)
                .IsValidPersianDate("تاریخ مانیفست");

        RuleFor(x => x.CompanySenderDescription)
           .NotNull().WithMessage("توضیحات شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
           .NotNull().WithMessage("توضیحات چاپ شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات چاپ شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}

