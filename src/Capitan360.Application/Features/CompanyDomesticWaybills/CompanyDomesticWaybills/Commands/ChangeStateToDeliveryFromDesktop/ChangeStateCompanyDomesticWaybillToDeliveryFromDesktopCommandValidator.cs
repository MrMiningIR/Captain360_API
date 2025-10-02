using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDeliveryFromDesktop;

public class ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommandValidator : AbstractValidator<ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommand>
{
    public ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گبرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گبرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گبرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.CompanyReceiverDateFinancial)
           .IsValidPersianDate("تاریخ تحویل مالی");

        RuleFor(x => x)
           .Must(x => x.CompanyReceiverCashPayment ||
                      x.CompanyReceiverCashOnDelivery ||
                      x.CompanyReceiverBankPayment ||
                      x.CompanyReceiverCreditPayment)
           .WithMessage("حداقل یکی از روش‌های پرداخت باید انتخاب شود"); 
        
        RuleFor(x => x.CompanyReceiverBankCode)
            .NotNull().WithMessage("کد بانک نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد بانک الزامی است")
            .MaximumLength(30).WithMessage("کد بانک نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .When(x => x.CompanyReceiverBankPayment);

        RuleFor(x => x.CompanyReceiverBankPaymentNo)
            .NotNull().WithMessage("کد پرداخت بانک نمی تواند خالی باشد است.")
            .MaximumLength(50).WithMessage("کد پرداخت بانک نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.CompanyReceiverBankPayment);

        RuleFor(x => x.CompanyReceiverResponsibleCustomerMobile)
            .NotNull().WithMessage("شماره تلفن مشتری نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("شماره تلفن مشتری الزامی است")
            .MaximumLength(11).WithMessage("شماره تلفن مشتری نمی‌تواند بیشتر از 11 کاراکتر باشد")
            .When(x => x.CompanyReceiverCreditPayment);

        RuleFor(x => x.EntranceDeliveryPerson)
            .NotNull().WithMessage("تحویل دهنده نمی تواند خالی باشد است.")
            .MaximumLength(100).WithMessage("تحویل دهنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.EntranceTransfereePersonName)
           .NotNull().WithMessage("نام تحویل گیرنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("نام تحویل گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.EntranceTransfereePersonNationalCode)
           .NotNull().WithMessage("کد ملی تحویل گیرنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("کد ملی تحویل گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionReceiverCompany)
           .NotNull().WithMessage("توضیحات شرکت نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DateDelivery)
            .IsValidPersianDate("تاریخ تحویل");

        RuleFor(x => x.TimeDelivery)
            .IsValidTime("ساعت تحویل");
    }
}
