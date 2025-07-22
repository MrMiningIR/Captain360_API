using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;

public class GetCompanyCommissionsByIdQueryValidator : AbstractValidator<GetCompanyCommissionsByIdQuery>
{
    public GetCompanyCommissionsByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه کمیسیون باید بزرگ‌تر از صفر باشد");
    }
}