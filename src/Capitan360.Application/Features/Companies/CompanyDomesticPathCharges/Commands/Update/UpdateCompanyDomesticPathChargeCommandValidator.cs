using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Update;

public class UpdateCompanyDomesticPathChargeCommandValidator : AbstractValidator<UpdateCompanyDomesticPathChargeCommand>
{
    private readonly List<WeightType> validWeightTypes = new List<WeightType>
    {
        WeightType.TypeMin, WeightType.TypeNormal, WeightType.TypeOne, WeightType.TypeTwo, WeightType.TypeThree,
        WeightType.TypeFour, WeightType.TypeFive, WeightType.TypeSix, WeightType.TypeSeven, WeightType.TypeEight,
        WeightType.TypeNine, WeightType.TypeTen, WeightType.TypeEleven
    };

    public UpdateCompanyDomesticPathChargeCommandValidator()
    {
        // شرط 1: لیست ChargeItems نباید null یا خالی باشد
        RuleFor(command => command.ChargeItems)
            .NotNull()
            .NotEmpty()
            .WithMessage("لیست آیتم‌های شارژ نمی‌تواند خالی باشد.");

        // شرط 2: باید آیتم‌هایی با WeightType.TypeMin و WeightType.TypeNormal وجود داشته باشد
        // و وزن‌ها و قیمت‌ها برای این WeightTypeها باید مثبت و افزایشی باشند
        RuleFor(command => command.ChargeItems)
            .Must(items =>
            {
                var minWeightItem = items.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var normalWeightItem = items.FirstOrDefault(item => item.WeightType == WeightType.TypeNormal);
                if (minWeightItem == null || normalWeightItem == null)
                    return false;

                return minWeightItem.Weight > 0 &&
                       normalWeightItem.Weight > 0 &&
                       minWeightItem.Price > 0 &&
                       normalWeightItem.Price > 0 &&
                       normalWeightItem.Weight > minWeightItem.Weight &&
                       normalWeightItem.Price > minWeightItem.Price;
            })
            .WithMessage($"لیست باید حداقل شامل آیتم‌هایی با {WeightType.TypeMin.GetDisplayName()} و {WeightType.TypeNormal.GetDisplayName()} باشد و وزن‌ها و قیمت‌ها باید مثبت و افزایشی باشند.");

        // شرط 3: قیمت در ContentItemsList برای WeightTypeها باید با وزن و قیمت مثبت در WeightType.TypeMin و WeightType.TypeNormal مطابقت داشته باشد
        RuleFor(command => command.ChargeItems)
            .Must(items =>
            {
                var minWeightItem = items.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var normalWeightItem = items.FirstOrDefault(item => item.WeightType == WeightType.TypeNormal);
                if (minWeightItem == null || normalWeightItem == null)
                    return true;

                foreach (var item in items)
                {
                    if (item.ContentItems != null && item.ContentItems.ContentItemsList != null)
                    {
                        foreach (var contentItem in item.ContentItems.ContentItemsList.Where(ci => ci.ContentTypeId > 0))
                        {
                            if (contentItem.Price > 0)
                            {
                                var weightItem = items.FirstOrDefault(i => i.WeightType == contentItem.WeightType);
                                if (weightItem == null || weightItem.Weight <= 0 || weightItem.Price <= 0)
                                    return false;
                            }
                        }
                    }
                }
                return true;
            })
            .WithMessage(command =>
            {
                var minWeightItem = command.ChargeItems.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var normalWeightItem = command.ChargeItems.FirstOrDefault(item => item.WeightType == WeightType.TypeNormal);
                if (minWeightItem == null)
                    return $"آیتم با {WeightType.TypeMin.GetDisplayName()} وجود ندارد.";
                if (normalWeightItem == null)
                    return $"آیتم با {WeightType.TypeNormal.GetDisplayName()} وجود ندارد.";

                var invalidWeightTypes = validWeightTypes
                    .Where(wt => command.ChargeItems.Any(item =>
                        item.ContentItems != null && item.ContentItems.ContentItemsList != null &&
                        item.ContentItems.ContentItemsList.Any(ci => ci.WeightType == wt && ci.ContentTypeId > 0 && ci.Price > 0) &&
                        command.ChargeItems.All(i => i.WeightType != wt || i.Weight <= 0 || i.Price <= 0)))
                    .Select(wt => wt.GetDisplayName());

                return $"در آیتم‌های با ContentTypeId بزرگ‌تر از 0، مقدار قیمت فقط برای {string.Join(", ", invalidWeightTypes)} می‌تواند وارد شود که وزن و قیمت متناظر مثبت باشند.";
            });

        // شرط 4: وزن‌ها باید به ترتیب افزایشی باشند
        RuleFor(command => command.ChargeItems)
            .Must(items =>
            {
                var rowItems = items.Where(item => item.Weight > 0)
                    .OrderBy(item => (int)item.WeightType)
                    .ToList();

                var typeMinItem = rowItems.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var typeNormalItem = rowItems.FirstOrDefault(item => item.WeightType == WeightType.TypeNormal);
                if (typeMinItem != null && typeNormalItem != null && typeNormalItem.Weight <= typeMinItem.Weight)
                    return false;

                for (int i = 1; i < rowItems.Count; i++)
                {
                    if (rowItems[i].Weight <= rowItems[i - 1].Weight)
                        return false;
                }
                return true;
            })
            .WithMessage(command =>
            {
                var rowItems = command.ChargeItems
                    .Where(item => item.Weight > 0)
                    .OrderBy(item => (int)item.WeightType)
                    .ToList();

                var errors = new List<string>();
                var typeMinItem = rowItems.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var typeNormalItem = rowItems.FirstOrDefault(item => item.WeightType == WeightType.TypeNormal);
                if (typeMinItem != null && typeNormalItem != null && typeNormalItem.Weight <= typeMinItem.Weight)
                    errors.Add($"وزن برای {WeightType.TypeNormal.GetDisplayName()} باید از {WeightType.TypeMin.GetDisplayName()} بیشتر باشد.");

                for (int i = 1; i < rowItems.Count; i++)
                {
                    if (rowItems[i].Weight <= rowItems[i - 1].Weight)
                        errors.Add($"وزن برای {rowItems[i].WeightType.GetDisplayName()} باید از {rowItems[i - 1].WeightType.GetDisplayName()} بیشتر باشد.");
                }

                return $"وزن‌ها باید به ترتیب افزایشی باشند: {string.Join("; ", errors)}";
            });

        // ولیدیشن برای هر آیتم
        RuleForEach(command => command.ChargeItems)
            .SetValidator(new UpdateCompanyDomesticPathChargeItemCommandValidator());
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var context = ValidationContext<UpdateCompanyDomesticPathChargeCommand>.CreateWithOptions((UpdateCompanyDomesticPathChargeCommand)model, x => x.IncludeProperties(propertyName));
        var result = await ValidateAsync(context);
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}