using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;

public class GetAllAddressQueryValidator : AbstractValidator<GetAllAddressQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(AddressDto.Order)];

    public GetAllAddressQueryValidator()
    {
        RuleFor(x => x)
            .Must(c => (c.CompanyId.HasValue) ^ !string.IsNullOrWhiteSpace(c.UserId))
            .WithMessage("آدرس باید مربوط به شرکت یا کاربر باشد");

        RuleFor(x => x.CompanyId!.Value)
            .GreaterThan(0)
            .When(x => x.CompanyId.HasValue)
            .WithMessage("شماره شناسایی شرکت باید بزرگتر از ضفر باشد");

        RuleFor(x => x.UserId!)
            .NotEmpty()
            .MaximumLength(450).WithMessage("حداکثر طول شناسه کاربر 450 کاراکتر است.")
            .When(x => !string.IsNullOrWhiteSpace(x.UserId));

        RuleFor(x => x.Active)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت شرکت می تواند یکی از حالت فعال، غیر فعال و یا همه باشد.");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"تعداد ایتم در صفحه باید یکی از موارد زیر باشد [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب سازی باید بر اساس یکی از آیتم های زیر باشد [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}

