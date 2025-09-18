using FluentValidation;

namespace Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{

    public GetUserByIdQueryValidator()
    {

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربر نمبتواند خالی باشد");

    }

}