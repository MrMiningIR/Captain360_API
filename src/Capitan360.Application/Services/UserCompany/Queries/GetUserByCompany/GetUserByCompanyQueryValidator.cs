using FluentValidation;

namespace Capitan360.Application.Services.UserCompany.Queries.GetUserByCompany;

public class GetUserByCompanyQueryValidator : AbstractValidator<GetUserByCompanyQuery>
{

    public GetUserByCompanyQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0)
            .WithMessage("شناسه کمپانی نمبتواند خالی باشد");


        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربر نمبتواند خالی باشد");

    }

}