using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetCompanyTypeById;

public class GetCompanyTypeByIdQueryValidator : AbstractValidator<GetCompanyTypeByIdQuery>
{
    public GetCompanyTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");
    }
}