using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetById;

public class GetCompanyDomesticPathByIdQueryValidator : AbstractValidator<GetCompanyDomesticPathByIdQuery>
{
    public GetCompanyDomesticPathByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");
    }
}