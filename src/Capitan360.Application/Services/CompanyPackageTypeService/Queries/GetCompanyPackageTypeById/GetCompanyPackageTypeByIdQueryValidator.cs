using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;

public class GetCompanyPackageTypeByIdQueryValidator : AbstractValidator<GetCompanyPackageTypeByIdQuery>
{
    public GetCompanyPackageTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع محتوی باید بزرگ‌تر از صفر باشد");
    }
}