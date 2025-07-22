using Capitan360.Application.Services.AddressService.Dtos;
using FluentValidation;

namespace Capitan360.Application.Services.AddressService.Queries.GetAllAddresses;

public class GetAllAddressQueryValidator : AbstractValidator<GetAllAddressQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];

    private string[] _allowedSortByColumnNames = [
        nameof(AddressDto.AddressLine),
        nameof(AddressDto.OrderAddress),
        //nameof(AddressDto.CountryId),
        //nameof(AddressDto.AddressType)
    ];

    public GetAllAddressQueryValidator()
    {
        RuleFor(r => r.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید مشخص باشد");
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}