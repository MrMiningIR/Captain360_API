using System.Text.RegularExpressions;
using Capitan360.Application.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;

public class ChangeStateCompanyDomesticWaybillToDeliveryCommandValidator : AbstractValidator<ChangeStateCompanyDomesticWaybillToDeliveryCommand>
{
    public ChangeStateCompanyDomesticWaybillToDeliveryCommandValidator()
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

        RuleFor(x => x.CompanyReceiverBankPaymentNo)
            .NotNull().WithMessage("کد پرداخت بانک نمی تواند خالی باشد است.")
            .MaximumLength(50).WithMessage("کد پرداخت بانک نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.CompanyReceiverBankPayment);

        RuleFor(x => x.CompanyReceiverResponsibleCustomerId)
            .NotEmpty().WithMessage("شماره شناسایی مشتری الزامی است")
            .MaximumLength(450).WithMessage("شماره شناسایی مشتری نمی‌تواند بیشتر از 450 کاراکتر باشد")
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
    }
}