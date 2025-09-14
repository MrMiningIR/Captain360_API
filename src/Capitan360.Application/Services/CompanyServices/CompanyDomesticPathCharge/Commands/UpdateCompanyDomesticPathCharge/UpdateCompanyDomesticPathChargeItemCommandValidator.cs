using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;

public class UpdateCompanyDomesticPathChargeItemCommandValidator : AbstractValidator<UpdateCompanyDomesticPathChargeItemCommand>
{
    private readonly List<WeightType> validWeightTypes = new List<WeightType>
    {
        WeightType.TypeMin, WeightType.TypeNormal, WeightType.TypeOne, WeightType.TypeTwo, WeightType.TypeThree,
        WeightType.TypeFour, WeightType.TypeFive, WeightType.TypeSix, WeightType.TypeSeven, WeightType.TypeEight,
        WeightType.TypeNine, WeightType.TypeTen, WeightType.TypeEleven
    };

    public UpdateCompanyDomesticPathChargeItemCommandValidator()
    {
        // شرط 5: WeightType باید معتبر باشد
        RuleFor(item => item.WeightType)
            .Must(wt => validWeightTypes.Contains(wt))
            .WithMessage(item => $"نوع وزن (WeightType) باید یکی از مقادیر معتبر {string.Join(", ", validWeightTypes.Select(wt => wt.GetDisplayName()))} باشد.");

        // شرط 6: وزن‌ها باید غیرمنفی باشند
        RuleFor(item => item.Weight)
            .GreaterThanOrEqualTo(0)
            .WithMessage(item => $"وزن برای {item.WeightType.GetDisplayName()} باید غیرمنفی باشد.");

        // شرط 7: قیمت برای WeightType.TypeMin باید مثبت باشد
        RuleFor(item => item)
            .Must(item => item.WeightType != WeightType.TypeMin || item.Price > 0)
            .WithMessage(item => $"قیمت برای {WeightType.TypeMin.GetDisplayName()} باید مثبت باشد.");

        // شرط 8: قیمت‌ها باید غیرمنفی باشند
        RuleFor(item => item.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(item => $"قیمت برای {item.WeightType.GetDisplayName()} باید غیرمنفی باشد.");

        // شرط 9: ContentItems نباید null باشد
        RuleFor(item => item.ContentItems)
            .NotNull()
            .WithMessage("ContentItems نمی‌تواند null باشد.");

        // شرط 10: قیمت‌ها در ContentItemsList باید غیرمنفی و افزایشی باشند
        RuleFor(item => item)
            .Must(item =>
            {
                if (item.ContentItems?.ContentItemsList == null || !item.ContentItems.ContentItemsList.Any())
                    return true;

                var areas = item.ContentItems.ContentItemsList
                    .Where(ci => ci.ContentTypeId > 0 && validWeightTypes.Contains(ci.WeightType))
                    .OrderBy(ci => (int)ci.WeightType)
                    .ToList();

                for (int i = 1; i < areas.Count; i++)
                {
                    if (areas[i].Price > 0 && areas[i - 1].Price > 0 && areas[i].Price <= areas[i - 1].Price)
                        return false;
                }
                return true;
            })
            .WithMessage(item =>
            {
                var areas = item.ContentItems?.ContentItemsList
                    .Where(ci => ci.ContentTypeId > 0 && validWeightTypes.Contains(ci.WeightType))
                    .OrderBy(ci => (int)ci.WeightType)
                    .ToList() ?? new List<UpdateCompanyDomesticPathContentItemCommand>();

                var errors = new List<string>();
                for (int i = 1; i < areas.Count; i++)
                {
                    if (areas[i].Price > 0 && areas[i - 1].Price > 0 && areas[i].Price <= areas[i - 1].Price)
                        errors.Add($"قیمت برای {areas[i].WeightType.GetDisplayName()} باید از {areas[i - 1].WeightType.GetDisplayName()} بیشتر باشد.");
                }
                return $"در ContentItemsList، قیمت‌های غیرصفر باید به ترتیب افزایشی باشند: {string.Join("; ", errors)}";
            });

        // شرط 11: قیمت‌ها در ContentItemsList باید غیرمنفی باشند
        RuleFor(item => item)
            .Must(item => item.ContentItems?.ContentItemsList?.All(ci => ci.Price >= 0) ?? true)
            .WithMessage($"در ContentItemsList، مقادیر قیمت اگر وارد شده باشند، باید غیرمنفی باشند.");
    }
}