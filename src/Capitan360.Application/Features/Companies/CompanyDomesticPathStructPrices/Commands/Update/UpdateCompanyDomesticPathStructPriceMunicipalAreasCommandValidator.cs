using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Update;

public class UpdateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator : AbstractValidator<UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand>
{
    public UpdateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator()
    {
        // شرط 1: اگر لیست خالی نیست، آیتم‌ها باید معتبر باشند
        RuleFor(command => command.DomesticPathStructPriceMunicipalAreas)
            .NotNull()
            .WithMessage("لیست آیتم‌های StructPriceArea نمی‌تواند null باشد.");

        // شرط 2: اعتبارسنجی فیلدهای CreateCompanyDomesticPathStructPriceMunicipalAreasItem
        RuleForEach(command => command.DomesticPathStructPriceMunicipalAreas)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.CompanyDomesticPathId)
                    .GreaterThan(0)
                    .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.");

                item.RuleFor(x => x.MunicipalAreaId)
                    .GreaterThan(0)
                    .WithMessage("شناسه منطقه شهری (MunicipalAreaId) باید بزرگ‌تر از صفر باشد.");

                item.RuleFor(x => (int)x.PathStructType)
                    .GreaterThan(0)
                    .WithMessage("نوع ساختار مسیر (PathStructType) باید بزرگ‌تر از صفر باشد.");

                item.RuleFor(x => (int)x.WeightType)
                    .GreaterThan(0)
                    .WithMessage("نوع وزن (WeightType) باید بزرگ‌تر از صفر باشد.");

                item.RuleFor(x => x.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(x => $"قیمت (Price) برای آیتم با WeightType={x.WeightType} نباید منفی باشد.");

                item.RuleFor(x => x.CompanyDomesticPathStructPriceId)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("شناسه قیمت ساختار مسیر (CompanyDomesticPathStructPriceId) باید بزرگ‌تر یا برابر با صفر باشد.");
            });
    }
}