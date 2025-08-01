using Capitan360.Domain.Constants;
using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Update;

public class UpdateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<UpdateCompanyDomesticPathListStructPriceCommand>
{
    private readonly List<int> validWeightTypes = new List<int> { -1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

    public UpdateCompanyDomesticPathListStructPriceCommandValidator()
    {
        // شرط 1: جدول یا باید خالی باشد یا حداقل ردیف 0 با وزن‌های مثبت برای WeightType -1 و 2 داشته باشد
        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
            .Must(items => items == null || !items.Any() ||
                           items.Any(item => item.MunicipalAreaId == 0 && item.WeightType == WeightType.TypeMin && item.Weight > 0) &&
                            items.Any(item => item.MunicipalAreaId == 0 && item.WeightType == WeightType.TypeTwo && item.Weight > 0))
            .WithMessage($"جدول یا باید خالی باشد یا ردیف با MunicipalAreaId برابر 0 باید مقادیر Weight مثبت برای ستون‌های {WeightType.TypeMin.GetDisplayName()} و {WeightType.TypeTwo.GetDisplayName()} داشته باشد.");

        // شرط 2: وزن‌های غیرصفر در ردیف 0 باید به ترتیب افزایشی باشند
        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
            .Must(items =>
            {
                var rowZeroItems = items.Where(item => item.MunicipalAreaId == 0 && item.Weight > 0)
                    .OrderBy(item => (int)item.WeightType)
                    .ToList();

                // بررسی وزن TypeTwo نسبت به TypeMin
                var typeMinItem = rowZeroItems.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var typeTwoItem = rowZeroItems.FirstOrDefault(item => item.WeightType == WeightType.TypeTwo);
                if (typeMinItem != null && typeTwoItem != null && typeTwoItem.Weight <= typeMinItem.Weight)
                {
                    return false;
                }

                // بررسی افزایشی بودن وزن‌های سایر WeightTypeها
                for (int i = 1; i < rowZeroItems.Count; i++)
                {
                    if (rowZeroItems[i].Weight <= rowZeroItems[i - 1].Weight)
                    {
                        return false;
                    }
                }

                return true;
            })
            .WithMessage(command =>
            {
                var rowZeroItems = command.UpdateCompanyDomesticPathStructPriceItems
                    .Where(item => item.MunicipalAreaId == 0 && item.Weight > 0)
                    .OrderBy(item => (int)item.WeightType)
                    .ToList();

                var errors = new List<string>();
                var typeMinItem = rowZeroItems.FirstOrDefault(item => item.WeightType == WeightType.TypeMin);
                var typeTwoItem = rowZeroItems.FirstOrDefault(item => item.WeightType == WeightType.TypeTwo);
                if (typeMinItem != null && typeTwoItem != null && typeTwoItem.Weight <= typeMinItem.Weight)
                {
                    errors.Add($"وزن برای {WeightType.TypeTwo.GetDisplayName()} باید از {WeightType.TypeMin.GetDisplayName()} بیشتر باشد.");
                }

                for (int i = 1; i < rowZeroItems.Count; i++)
                {
                    if (rowZeroItems[i].Weight <= rowZeroItems[i - 1].Weight)
                    {
                        errors.Add($"وزن برای {rowZeroItems[i].WeightType.GetDisplayName()} باید از {rowZeroItems[i - 1].WeightType.GetDisplayName()} بیشتر باشد.");
                    }
                }

                return $"در ردیف با MunicipalAreaId برابر 0، وزن‌های غیرصفر باید به ترتیب افزایشی باشند: {string.Join("; ", errors)}";
            });

        // شرط 3: قیمت در ردیف‌های غیرصفر باید با وزن مثبت در ردیف 0 مطابقت داشته باشد
        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
            .Must(items =>
            {
                var rowZeroItems = items.Where(item => item.MunicipalAreaId == 0).ToList();
                if (!rowZeroItems.Any()) return true;

                foreach (var item in items.Where(item => item.MunicipalAreaId != 0))
                {
                    if (item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.Any(area => area.Price > 0) == true)
                    {
                        foreach (var area in item.StructPriceArea.DomesticPathStructPriceMunicipalAreas)
                        {
                            if (area.Price > 0)
                            {
                                var rowZeroWeight = rowZeroItems.FirstOrDefault(row => row.WeightType == area.WeightType)?.Weight;
                                if (rowZeroWeight == null || rowZeroWeight <= 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                return true;
            })
            .WithMessage(command =>
            {
                var rowZeroItems = command.UpdateCompanyDomesticPathStructPriceItems.Where(item => item.MunicipalAreaId == 0).ToList();
                if (!rowZeroItems.Any()) return "ردیف با MunicipalAreaId برابر 0 وجود ندارد.";
                var invalidWeightTypes = validWeightTypes.Where(wt => command.UpdateCompanyDomesticPathStructPriceItems
                        .Any(item => item.MunicipalAreaId != 0 &&
                                     item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.Any(area => area.WeightType == (WeightType)wt && area.Price > 0) == true &&
                                     !rowZeroItems.Any(row => row.WeightType == (WeightType)wt && row.Weight > 0)))
                    .Select(wt => ((WeightType)wt).GetDisplayName());
                return $"در ردیف‌های با MunicipalAreaId غیرصفر، مقدار Price فقط برای ستون‌های {string.Join(", ", invalidWeightTypes)} می‌تواند وارد شود که مقدار Weight متناظر در ردیف با MunicipalAreaId برابر 0 مثبت باشد.";
            });

        // ولیدیشن برای هر آیتم
        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
            .SetValidator(new UpdateCompanyDomesticPathStructPriceItemCommandValidator());
    }
}