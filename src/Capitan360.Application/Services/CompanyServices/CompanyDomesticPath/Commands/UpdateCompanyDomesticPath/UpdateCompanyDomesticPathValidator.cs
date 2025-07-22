using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;

public class UpdateCompanyDomesticPathValidator : AbstractValidator<UpdateCompanyDomesticPathCommand>
{
    public UpdateCompanyDomesticPathValidator()
    {
        RuleFor(x => x.CompanyId)
    .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات الزامی است")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionForSearch)
            .NotEmpty().WithMessage("توضیحات جستجو الزامی است")
            .MaximumLength(500).WithMessage("توضیحات جستجو نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.EntranceFee)
            .NotNull().NotEmpty().WithMessage("نمیتواند بدون مقدار باشد")
            .GreaterThanOrEqualTo(0).WithMessage("هزینه ورودی نمی‌تواند منفی باشد").LessThanOrEqualTo(1_000_000_000).WithMessage("هزینه ورودی نمی‌تواند بیشتر از یک میلیارد باشد"); ;

        RuleFor(x => x.EntranceWeight)
             .NotNull().NotEmpty().WithMessage("نمیتواند بدون مقدار باشد")
            .GreaterThanOrEqualTo(0).WithMessage("وزن ورودی نمی‌تواند منفی باشد").InclusiveBetween(0m, 99_999_999.99m).WithMessage("وزن ورودی باید بین 0 و 99,999,999.99 باشد")
            .ScalePrecision(2, 10).WithMessage("وزن ورودی باید حداکثر 10 رقم با 2 رقم اعشار داشته باشد");

        RuleFor(x => x.EntranceType)
           .GreaterThan(0).WithMessage("نوع ورودی معتبر نیست");

        RuleFor(x => x.SourceCountryId)
            .GreaterThan(0).WithMessage("شناسه کشور مبدا الزامی است");

        RuleFor(x => x.SourceProvinceId)
            .GreaterThan(0).WithMessage("شناسه استان مبدا الزامی است");

        RuleFor(x => x.SourceCityId)
            .GreaterThan(0).WithMessage("شناسه شهر مبدا الزامی است");

        RuleFor(x => x.DestinationCountryId)
            .GreaterThan(0).WithMessage("شناسه کشور مقصد الزامی است");

        RuleFor(x => x.DestinationProvinceId)
            .GreaterThan(0).WithMessage("شناسه استان مقصد الزامی است");

        RuleFor(x => x.DestinationCityId)
            .GreaterThan(0).WithMessage("شناسه شهر مقصد الزامی است");
    }
}