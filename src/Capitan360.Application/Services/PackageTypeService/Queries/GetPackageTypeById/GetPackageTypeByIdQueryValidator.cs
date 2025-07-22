using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Queries.GetPackageTypeById;

public class GetPackageTypeByIdQueryValidator : AbstractValidator<GetPackageTypeByIdQuery>
{
    public GetPackageTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع محتوا باید بزرگ‌تر از صفر باشد");
    }
}