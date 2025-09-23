using Capitan360.Application.Extensions;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.UpdateDomesticWaybill;

public class UpdateDomesticWaybillCommandValidator : AbstractValidator<UpdateDomesticWaybillCommand>
{
    public UpdateDomesticWaybillCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شماره شناسایی بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x)
            .Must(x =>
                (x.CompanyReceiverId.HasValue &&
                 string.IsNullOrEmpty(x.CompanyReceiverCaptain360Code) &&
                 string.IsNullOrEmpty(x.CompanyReceiverCaptain360Name))
                ||
                (!x.CompanyReceiverId.HasValue &&
                 !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Code) &&
                 !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Name))
            )
            .WithMessage("شرکت گیرنده معتبر نیست");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .MinimumLength(1)
            .When(x => !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Code))
            .WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Name)
            .MinimumLength(1)
            .When(x => !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Name))
            .WithMessage("نام کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.InsuranceCompanyId)
            .NotEmpty().WithMessage("شماره شناسایی شرکت بیمه کاپیتان 360 الزامی است")
            .When(x => x.ExitInsuranceCost > 0 ||
                       x.ExitTaxInsuranceCost > 0 ||
                       x.ExitInsuranceCostGain > 0 ||
                       x.ExitTaxInsuranceCostGain > 0);

        RuleFor(x => x.GrossWeight)
            .GreaterThan(0).WithMessage("وزن محاسبه شده باید بزرگتر از صفر باشد")
            .PrecisionScale(10, 2, true).WithMessage("وزن محاسبه شده باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

        RuleFor(x => x.ChargeableWeight)
            .GreaterThan(0).WithMessage("وزن محاسبه شده باید بزرگتر از صفر باشد")
            .PrecisionScale(10, 2, true).WithMessage("وزن محاسبه شده باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

        RuleFor(x => x.DimensionalWeight)
           .GreaterThanOrEqualTo(0).WithMessage("وزن ابعادی باید بزرگتر یا مساوری صفر باشد")
           .PrecisionScale(10, 2, true).WithMessage("وزن ابعادی باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

        RuleFor(x => x.WeightCount)
            .GreaterThan(0).WithMessage("تعداد مرسولات باید بزرگتر از صفر باشد");

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0).WithMessage("نرخ باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.CompanyDomesticWaybillTax)
            .InclusiveBetween(0m, 100m).WithMessage("نرخ مالیات باید بین 0 تا 100 باشد.")
            .PrecisionScale(5, 2, true).WithMessage("نرخ مالیات باید حداکثر 2 رقم اعشار و مجموعاً 5 رقم داشته باشد.");

        RuleFor(x => x.ExitFare)
            .GreaterThanOrEqualTo(0).WithMessage("کرایه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitStampBill)
            .GreaterThanOrEqualTo(0).WithMessage("تمبر بارنامه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitPackaging)
           .GreaterThanOrEqualTo(0).WithMessage("بسته بندی باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitAccumulation)
            .GreaterThanOrEqualTo(0).WithMessage("جمع آوری باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitDistribution)
            .GreaterThanOrEqualTo(0).WithMessage("توزیع باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitExtraSource)
            .GreaterThanOrEqualTo(0).WithMessage("متفرقه مبدا باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitExtraDestination)
            .GreaterThanOrEqualTo(0).WithMessage("متفرقه مقصد باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitPricing)
            .GreaterThanOrEqualTo(0).WithMessage("قیمت گذاری باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitRevenue1)
            .GreaterThanOrEqualTo(0).WithMessage("هزینه متفرقه 1 باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitRevenue2)
            .GreaterThanOrEqualTo(0).WithMessage("هزینه متفرقه 2 باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitRevenue3)
            .GreaterThanOrEqualTo(0).WithMessage("هزینه متفرقه 3 باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitTaxCompanySender)
            .GreaterThanOrEqualTo(0).WithMessage("مالیات شرکت فرستنده باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitTaxCompanyReceiver)
            .GreaterThanOrEqualTo(0).WithMessage("مالیات شرکت گیرنده باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitInsuranceCost)
           .GreaterThanOrEqualTo(0).WithMessage("بیمه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitTaxInsuranceCost)
           .GreaterThanOrEqualTo(0).WithMessage("مالیات بیمه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitInsuranceCostGain)
            .GreaterThanOrEqualTo(0).WithMessage("سود بیمه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitTaxInsuranceCostGain)
            .GreaterThanOrEqualTo(0).WithMessage("مالیات سود بیمه باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitDiscount)
            .GreaterThanOrEqualTo(0).WithMessage("تخفیف باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x.ExitTotalCost)
            .GreaterThanOrEqualTo(0).WithMessage("جمع کل باید بزرگتر یا مساوری صفر باشد");

        RuleFor(x => x)
            .Must(x => x.ExitFare + x.ExitStampBill + x.ExitPackaging + x.ExitAccumulation + x.ExitDistribution +
                       x.ExitExtraSource + x.ExitExtraDestination + x.ExitPricing + x.ExitRevenue1 + x.ExitRevenue2 +
                       x.ExitRevenue3 + x.ExitTaxCompanySender + x.ExitTaxCompanyReceiver + +x.ExitInsuranceCost +
                       x.ExitTaxInsuranceCost + x.ExitInsuranceCostGain + x.ExitTaxInsuranceCostGain - x.ExitDiscount == x.ExitTotalCost
                 )
            .WithMessage("مجموع اقلام باید برابر با جمع کل باشد");

        RuleFor(x => x.HandlingInformation)
            .MaximumLength(4000).WithMessage("اطلاعات حمل بار نمی‌تواند بیشتر از 4000 کاراکتر باشد");

        RuleFor(x => x.FlightNo)
            .MaximumLength(50).WithMessage("شماره پرواز بار نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.FlightDate)
            .IsValidPersianDate("تاریخ پرواز");

        RuleFor(x => x.CompanySenderDateFinancial)
            .IsValidPersianDate("تاریخ ثبت");

        RuleFor(x => x)
           .Must(x => x.CompanySenderCashPayment ||
                      x.CompanySenderCashOnDelivery ||
                      x.CompanySenderBankPayment ||
                      x.CompanySenderCreditPayment)
           .WithMessage("حداقل یکی از روش‌های پرداخت باید انتخاب شود");

        RuleFor(x => x.CompanySenderBankId)
            .GreaterThan(0).WithMessage("شماره شناسیی بانک کاپیتان 360 الزامی است")
            .When(x => x.CompanySenderBankPayment);

        RuleFor(x => x.CustomerPanelId)
            .NotEmpty().WithMessage("شماره شناسایی مشتری الزامی است")
            .MaximumLength(450).WithMessage("شماره شناسایی مشتری نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderNameFamily)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSenderNameFamily))
            .WithMessage("نام و نام خانوادگی فرستنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderMobile)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSenderMobile))
            .WithMessage("شماره موبایل فرستنده نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderAddress)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSenderAddress))
            .WithMessage("آدرس فرستنده نمی‌تواند بیشتر از 1000 کاراکتر باشد");

        RuleFor(x => (int)x.TypeOfFactorInSamanehMoadianId)
           .Must(typeOfFactorInSamanehMoadianId => Enum.IsDefined(typeof(MoadianFactorType), (int)typeOfFactorInSamanehMoadianId))
           .WithMessage("نوع سامانه مودیان معتبر نیست");
        When(x => x.TypeOfFactorInSamanehMoadianId == (short)MoadianFactorType.Hoghoghi, () =>
        {
            RuleFor(x => x.CustomerSenderEconomicCode)
                .NotEmpty().WithMessage("شناسه ملی اقتصادی برای حقوقی الزامی است");

            RuleFor(x => x.CustomerSenderNationalID)
                .NotEmpty().WithMessage("شناسه ملی برای حقوقی الزامی است");
        });

        RuleFor(x => x.CustomerSenderNationalCode)
            .NotEmpty().WithMessage("کد ملی فرستنده الزامی است")
            .MaximumLength(50)
            .WithMessage("کد ملی فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderEconomicCode)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSenderEconomicCode))
            .WithMessage("کد اقتصادی فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderNationalID)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSenderNationalID))
            .WithMessage("شناسه ملی فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverNameFamily)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerReceiverNameFamily))
            .WithMessage("نام و نام خانوادگی گیرنده نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverMobile)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerReceiverMobile))
            .WithMessage("شماره موبایل گیرنده نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverAddress)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerReceiverAddress))
            .WithMessage("آدرس گیرنده نمی‌تواند بیشتر از 1000 کاراکتر باشد");

        RuleFor(x => x.DescriptionSenderCompany)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionSenderCompany))
            .WithMessage("توضیحات محتوی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionSenderCustomer)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionSenderCustomer))
            .WithMessage("توضیحات محتوی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x)
            .Must(x => x.WeightCount == (x.CompanyDomesticWaybillPackageTypes?.Count ?? 0))
            .WithMessage("تعداد بسته‌ها باید برابر با وزن شمارشی باشد");

        RuleFor(x => x)
            .Must(x => x.GrossWeight == (x.CompanyDomesticWaybillPackageTypes?.Sum(dp => dp.GrossWeight) ?? 0))
            .WithMessage("وزن خالص بسته‌ها باید برابر با وزن شمارشی باشد");

        RuleFor(x => x)
            .Must(x => x.DimensionalWeight == (x.CompanyDomesticWaybillPackageTypes?.Sum(dp => dp.DimensionalWeight) ?? 0))
            .WithMessage("وزن ابعادی بسته‌ها باید برابر با وزن شمارشی باشد");

        RuleFor(x => x.StopCityName)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.StopCityName))
            .WithMessage("نام شهر میانی نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CompanySenderBankPaymentNo)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanySenderBankPaymentNo))
            .WithMessage("کد پرداخت بانک شرکت فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}

