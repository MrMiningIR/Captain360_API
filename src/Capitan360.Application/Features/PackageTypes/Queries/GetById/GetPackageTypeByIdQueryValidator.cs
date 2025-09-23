using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Queries.GetById;

public class GetPackageTypeByIdQueryValidator : AbstractValidator<GetPackageTypeByIdQuery>
{
    public GetPackageTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع بسته بندی باید بزرگتر از صفر باشد");
    }
}