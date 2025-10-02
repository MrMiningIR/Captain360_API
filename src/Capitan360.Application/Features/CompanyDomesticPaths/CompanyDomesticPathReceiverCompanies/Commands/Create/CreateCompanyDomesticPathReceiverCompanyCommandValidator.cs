using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;

public class CreateCompanyDomesticPathReceiverCompanyCommandValidator : AbstractValidator<CreateCompanyDomesticPathReceiverCompanyCommand>
{
    public CreateCompanyDomesticPathReceiverCompanyCommandValidator()
    {
        RuleFor(x => x.CompanyDomesticPathId)
            .GreaterThan(0).WithMessage("شناسه مسیر الزامی است");

        RuleFor(x => x.MunicipalAreaId)
            .GreaterThan(0).WithMessage("شناسه منطقه جغرافیایی الزامی است");

        RuleFor(x => x)
        .Must(x =>
            // حالت 1: همه null
            x.ReceiverCompanyId == null &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedCode) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedName) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedTelephone) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedAddress)

            ||

            // حالت 2: فقط Id مقدار دارد
            x.ReceiverCompanyId != null &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedCode) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedName) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedTelephone) &&
             string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedAddress)

            ||

            // حالت 3: فقط Code و Name مقدار دارند (حداقل 1 کاراکتر)
            x.ReceiverCompanyId == null &&
             !string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedCode) &&
             !string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedName) &&
             !string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedTelephone) &&
             !string.IsNullOrWhiteSpace(x.ReceiverCompanyUserInsertedAddress) &&
             x.ReceiverCompanyUserInsertedCode.Length >= 1 &&
             x.ReceiverCompanyUserInsertedName.Length >= 1 &&
             x.ReceiverCompanyUserInsertedTelephone.Length >= 1 &&
             x.ReceiverCompanyUserInsertedAddress.Length >= 1
        )
        .WithMessage("باید یکی از سه حالت معتبر انتخاب شود: (فقط شرکت مقصد)، (فقط اطلاعات نماینده)، یا هیچ‌کدام.");

        RuleFor(x => x.ReceiverCompanyUserInsertedCode)
            .MaximumLength(10).When(x => x.ReceiverCompanyUserInsertedCode != null)
            .WithMessage("کد نماینده باید کمتر یا مساوی از 10 کاراکتر باشد.");

        RuleFor(x => x.ReceiverCompanyUserInsertedName)
            .MaximumLength(50).When(x => x.ReceiverCompanyUserInsertedName != null)
            .WithMessage("نام نماینده باید کمتر یا مساوی از 50 کاراکتر باشد.");

        RuleFor(x => x.ReceiverCompanyUserInsertedTelephone)
            .MaximumLength(30).When(x => x.ReceiverCompanyUserInsertedTelephone != null)
            .WithMessage("تلفن نماینده باید کمتر یا مساوی از 30 کاراکتر باشد.");

        RuleFor(x => x.ReceiverCompanyUserInsertedAddress)
            .MaximumLength(200).When(x => x.ReceiverCompanyUserInsertedAddress != null)
            .WithMessage("آدرس نماینده باید کمتر یا مساوی از 200 کاراکتر باشد.");

        RuleFor(x => x.DescriptionForPrint1)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات 1 نباید بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionForPrint2)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات 2 نباید بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionForPrint3)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات 3 نباید بیشتر از 500 کاراکتر باشد");
    }
}