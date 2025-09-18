using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Update;

public class UpdateCompanyDomesticPathStructPriceItemCommandValidator : AbstractValidator<UpdateCompanyDomesticPathStructPriceItem>
{
    private readonly List<int> validWeightTypes = new List<int> { -1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

    public UpdateCompanyDomesticPathStructPriceItemCommandValidator()
    {
        validWeightTypes = validWeightTypes.OrderBy(wt => wt).ToList();

        // شرط 4: WeightType باید معتبر باشد
        RuleFor(item => item.WeightType)
            .Must(wt => validWeightTypes.Contains((int)wt))
            .WithMessage(item => $"نوع وزن (WeightType) باید یکی از مقادیر معتبر {string.Join(", ", validWeightTypes.Select(wt => ((WeightType)wt).GetDisplayName()))} باشد.");

        // ولیدیشن برای ردیف با MunicipalAreaId == 0 (وزن‌ها)
        When(item => item.MunicipalAreaId == 0, () =>
        {
            // شرط 5: WeightType -1 و 2 باید Weight مثبت داشته باشند
            RuleFor(item => item)
                .Must(item => !(item.WeightType == WeightType.TypeMin || item.WeightType == WeightType.TypeTwo) || item.Weight > 0)
                .WithMessage(item => $"در ردیف با MunicipalAreaId برابر 0، مقدار Weight برای ستون {item.WeightType.GetDisplayName()} باید مثبت باشد.");

            // شرط 6: وزن‌ها باید غیرمنفی باشند
            RuleFor(item => item.Weight)
                .GreaterThanOrEqualTo(0)
                .WithMessage(item => $"در ردیف با MunicipalAreaId برابر 0، مقدار Weight برای ستون {item.WeightType.GetDisplayName()} باید غیرمنفی باشد.");
        });

        // ولیدیشن برای ردیف‌های با MunicipalAreaId != 0 (قیمت‌ها)
        When(item => item.MunicipalAreaId != 0, () =>
        {
            // شرط 7: اگر Price برای WeightType -1 وارد شده، باید مثبت باشد
            RuleFor(item => item)
                .Must(item => item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.All(area => area.WeightType != WeightType.TypeMin || area.Price > 0) != false)
                .WithMessage(item => $"در ردیف‌های با MunicipalAreaId غیرصفر، مقدار Price برای ستون {WeightType.TypeMin.GetDisplayName()} اگر وارد شده باشد، باید مثبت باشد.");

            // شرط 8: قیمت‌های غیرصفر باید به ترتیب افزایشی باشند
            RuleFor(item => item)
                .Must(item =>
                {
                    if (item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.Any(area => area.Price > 0) != true)
                        return true;

                    var areas = item.StructPriceArea.DomesticPathStructPriceMunicipalAreas
                        .Where(a => validWeightTypes.Contains((int)a.WeightType))
                        .OrderBy(a => (int)a.WeightType)
                        .ToList();

                    for (int i = 1; i < areas.Count; i++)
                    {
                        if (areas[i].Price > 0 && areas[i - 1].Price > 0 && areas[i].Price <= areas[i - 1].Price)
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .WithMessage(item =>
                {
                    var areas = item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas
                        .Where(a => validWeightTypes.Contains((int)a.WeightType))
                        .OrderBy(a => (int)a.WeightType)
                        .ToList() ?? new List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem>();

                    var errors = new List<string>();
                    for (int i = 1; i < areas.Count; i++)
                    {
                        if (areas[i].Price > 0 && areas[i - 1].Price > 0 && areas[i].Price <= areas[i - 1].Price)
                        {
                            errors.Add($"قیمت برای {areas[i].WeightType.GetDisplayName()} باید از {areas[i - 1].WeightType.GetDisplayName()} بیشتر باشد.");
                        }
                    }
                    return $"در ردیف‌های با MunicipalAreaId غیرصفر، مقادیر غیرصفر Price باید به ترتیب افزایشی باشند: {string.Join("; ", errors)}";
                });

            // شرط 9: قیمت‌ها باید غیرمنفی باشند
            RuleFor(item => item)
                .Must(item => item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.All(area => area.Price >= 0) != false)
                .WithMessage($"در ردیف‌های با MunicipalAreaId غیرصفر، مقادیر Price اگر وارد شده باشند، باید غیرمنفی باشند.");
        });
    }
}