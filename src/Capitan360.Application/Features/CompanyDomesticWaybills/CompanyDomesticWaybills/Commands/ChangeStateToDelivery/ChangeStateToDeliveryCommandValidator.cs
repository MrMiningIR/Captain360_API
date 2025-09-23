using System.Text.RegularExpressions;
using Capitan360.Application.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;

public class ChangeStateToDeliveryCommandValidator : AbstractValidator<ChangeStateToDeliveryCommand>
{
    public ChangeStateToDeliveryCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverDateFinancial)
           .IsValidPersianDate("تاریخ تحویل مالی");

        RuleFor(x => x)
            .Must(x => x.CompanyReceiverCashPayment ||
                       x.CompanyReceiverCashOnDelivery ||
                       x.CompanyReceiverBankPayment ||
                       x.CompanyReceiverCreditPayment)
            .WithMessage("حداقل یکی از روش‌های پرداخت باید انتخاب شود");

        RuleFor(x => x.CompanyReceiverBankId)
            .GreaterThan(0).WithMessage("شماره شناسیی بانک کاپیتان 360 الزامی است")
            .When(x => x.CompanyReceiverBankPayment);

        RuleFor(x => x.CompanyReceiverResponsibleCustomerId)
            .NotEmpty().WithMessage("شماره شناسایی مشتری الزامی است")
            .MaximumLength(450).WithMessage("شماره شناسایی مشتری نمی‌تواند بیشتر از 450 کاراکتر باشد")
            .When(x => x.CompanyReceiverCreditPayment);

        RuleFor(x => x.EntranceDeliveryPerson)
            .NotEmpty().WithMessage("نام تحویل دهنده اجباری است")
            .MaximumLength(100)
            .WithMessage("نام تحویل دهنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.EntranceTransfereePersonName)
            .NotEmpty().WithMessage("نام تحویل گیرنده اجباری است")
            .MaximumLength(100)
            .WithMessage("نام تحویل گیرنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.EntranceTransfereePersonNationalCode)
            .NotEmpty().WithMessage("کد ملی تحویل گیرنده اجباری است")
            .MaximumLength(100)
            .WithMessage("کد ملی تحویل گیرنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.DescriptionReceiverCompany)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionReceiverCompany))
            .WithMessage("توضیحات شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanyReceiverBankPaymentNo)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyReceiverBankPaymentNo))
            .WithMessage("کد پرداخت بانک شرکت گیرنده نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}