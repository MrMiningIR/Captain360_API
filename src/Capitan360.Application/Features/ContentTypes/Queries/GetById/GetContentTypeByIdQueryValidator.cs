using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Queries.GetById;

public class GetContentTypeByIdQueryValidator : AbstractValidator<GetContentTypeByIdQuery>
{
    public GetContentTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}