using Capitan360.Application.Services.PackageTypeService.Dtos;
using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Queries.GetAllPackageTypes;

public class GetAllPackageTypesQueryValidator : AbstractValidator<GetAllPackageTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(PackageTypeDto.CompanyTypeName),
        nameof(PackageTypeDto.Active),
        nameof(PackageTypeDto.OrderPackageType)
    ];

    public GetAllPackageTypesQueryValidator()
    {


        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
        RuleFor(r => r.CompanyTypeId)
    .GreaterThanOrEqualTo(0);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}