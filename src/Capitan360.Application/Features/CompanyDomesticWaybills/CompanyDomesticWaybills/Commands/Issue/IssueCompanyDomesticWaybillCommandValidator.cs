using Capitan360.Application.Extensions;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Issue;

public class IssueCompanyDomesticWaybillCommandValidator : AbstractValidator<IssueCompanyDomesticWaybillCommand>
{
    public IssueCompanyDomesticWaybillCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگتر از صفر باشد");

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
                        context.AddFailure("کد نماینده نمی‌تواند خالی باشد");
                    }
                    else
                    {
                        if (!codeIsSet)
                            context.AddFailure("کد نماینده نمی‌تواند خالی باشد");
                    }
                }
            });

        RuleFor(x => x.SourceLongitude)
            .InclusiveBetween(-180m, 180m).WithMessage("طول جغرافیایی مبدا معتبر نیست.")
            .When(x => x.SourceLongitude.HasValue);

        RuleFor(x => x.SourceLatitude)
            .InclusiveBetween(-180m, 180m).WithMessage("عرض جغرافیایی مبدا معتبر نیست.")
            .When(x => x.SourceLatitude.HasValue);

        RuleFor(x => x.DestinationLongitude)
            .InclusiveBetween(-180m, 180m).WithMessage("طول جغرافیایی مقصد معتبر نیست.")
            .When(x => x.DestinationLongitude.HasValue);

        RuleFor(x => x.DestinationLatitude)
            .InclusiveBetween(-180m, 180m).WithMessage("عرض جغرافیایی مقصد معتبر نیست.")
            .When(x => x.DestinationLatitude.HasValue);

        RuleFor(x => x.InsuranceCompanyId)
           .NotEmpty().WithMessage("شماره شناسایی شرکت بیمه الزامی است")
           .When(x => x.ExitInsuranceCost > 0 ||
                      x.ExitTaxInsuranceCost > 0 ||
                      x.ExitInsuranceCostGain > 0 ||
                      x.ExitTaxInsuranceCostGain > 0);

        RuleFor(x => x.StopCityName)
            .NotNull().WithMessage("شهر میانی نمی تواند خالی باشد است.")
            .MaximumLength(30).WithMessage("شهر میانی نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.GrossWeight)
           .GreaterThan(0).WithMessage("وزن خالص باید بزرگتر از صفر باشد")
           .PrecisionScale(10, 2, true).WithMessage("وزن خالص باید حداکثر 2 رقم اعشار و مجموعاً 10 رقم داشته باشد.");

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
           .NotNull().WithMessage("اطلاعات حمل بار نمی تواند خالی باشد است.")
           .MaximumLength(4000).WithMessage("اطلاعات حمل بار نمی‌تواند بیشتر از 4000 کاراکتر باشد");

        RuleFor(x => x.FlightNo)
            .NotNull().WithMessage("شماره پرواز نمی تواند خالی باشد است.")
            .MaximumLength(50).WithMessage("شماره پرواز نمی‌تواند بیشتر از 50 کاراکتر باشد");

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
            .GreaterThan(0).WithMessage("شماره شناسیی بانک الزامی است")
            .When(x => x.CompanySenderBankPayment);

        RuleFor(x => x.CompanySenderBankPaymentNo)
            .NotNull().WithMessage("کد پرداخت نمی تواند خالی باشد است.")
            .MaximumLength(50).WithMessage("کد پرداخت نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.CompanySenderBankPayment);

        RuleFor(x => x.CustomerPanelId)
            .NotEmpty().WithMessage("شماره شناسایی مشتری الزامی است")
            .MaximumLength(450).WithMessage("شماره شناسایی مشتری نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderNameFamily)
            .NotEmpty().WithMessage("نام و نام خانوادگی فرستنده  الزامی است")
            .MaximumLength(100).WithMessage("نام و نام خانوادگی فرستنده  نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderMobile)
            .NotEmpty().WithMessage("شماره موبایل فرستنده الزامی است")
            .MaximumLength(30).WithMessage("شماره موبایل فرستنده نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CustomerSenderAddress)
            .NotEmpty().WithMessage("آدرس فرستنده الزامی است")
            .MaximumLength(1000).WithMessage("آدرس فرستنده نمی‌تواند بیشتر از 1000 کاراکتر باشد");

        RuleFor(x => (int)x.TypeOfFactorInSamanehMoadianId)
           .Must(typeOfFactorInSamanehMoadianId => Enum.IsDefined(typeof(MoadianFactorType), typeOfFactorInSamanehMoadianId))
           .WithMessage("نوع سامانه مودیان معتبر نیست");
        When(x => x.TypeOfFactorInSamanehMoadianId == (short)MoadianFactorType.Hoghoghi, () =>
        {
            RuleFor(x => x.CustomerSenderEconomicCode)
                .NotEmpty().WithMessage("کد اقتصادی برای حقوقی الزامی است")
                .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

            RuleFor(x => x.CustomerSenderNationalID)
                .NotEmpty().WithMessage("شناسه ملی برای حقوقی الزامی است")
                .MaximumLength(50).WithMessage("شناسه ملی برای حقوقی نمی‌تواند بیشتر از 50 کاراکتر باشد");
        });

        RuleFor(x => x.CustomerSenderNationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است")
            .MaximumLength(50).WithMessage("کد ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverNameFamily)
            .NotEmpty().WithMessage("نام و نام خانوادگی گیرنده  الزامی است")
            .MaximumLength(100).WithMessage("نام و نام خانوادگی گیرنده  نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverMobile)
            .NotEmpty().WithMessage("شماره موبایل گیرنده الزامی است")
            .MaximumLength(30).WithMessage("شماره موبایل گیرنده نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CustomerReceiverAddress)
            .NotEmpty().WithMessage("آدرس گیرنده الزامی است")
            .MaximumLength(1000).WithMessage("آدرس گیرنده نمی‌تواند بیشتر از 1000 کاراکتر باشد");

        RuleFor(x => x.DescriptionSenderCompany)
           .NotNull().WithMessage("توضیحات فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionSenderCustomer)
           .NotNull().WithMessage("توضیحات شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x)
            .Must(x =>
                x.IsIssueFromCaptainCargoWebSite ||
                x.IsIssueFromCompanyWebSite ||
                x.IsIssueFromCaptainCargoWebService ||
                x.IsIssueFromCompanyWebService ||
                x.IsIssueFromCaptainCargoPanel
            )
            .WithMessage("وضعیت پرداخت بارنامه مشخص نشده است");

        RuleForEach(x => x.CompanyDomesticWaybillPackageTypes)
            .SetValidator(new IssueCompanyDomesticWaybillPackageTypeCommandValidator());

        RuleFor(x => x)
            .Must(x => x.WeightCount == (x.CompanyDomesticWaybillPackageTypes?.Count ?? 0))
            .WithMessage("تعداد بسته‌ها باید برابر با وزن شمارشی باشد");

        RuleFor(x => x)
            .Must(x => x.GrossWeight == (x.CompanyDomesticWaybillPackageTypes?.Sum(dp => dp.GrossWeight) ?? 0))
            .WithMessage("وزن خالص بسته‌ها باید برابر با وزن شمارشی باشد");

        RuleFor(x => x)
            .Must(x => x.DimensionalWeight == (x.CompanyDomesticWaybillPackageTypes?.Sum(dp => dp.DimensionalWeight) ?? 0))
            .WithMessage("وزن ابعادی بسته‌ها باید برابر با وزن شمارشی باشد");
    }
}