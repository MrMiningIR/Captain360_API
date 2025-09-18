using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;

public class GetCompanyContentTypeByIdQueryValidator : AbstractValidator<GetCompanyContentTypeByIdQuery>
{
    public GetCompanyContentTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه نوع محتوی بار باید بزرگتر از صفر باشد");
    }
}