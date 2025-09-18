using FluentValidation;

namespace Capitan360.Application.Features.ContentTypeService.Queries.GetById;

public class GetContentTypeByIdQueryValidator : AbstractValidator<GetContentTypeByIdQuery>
{
    public GetContentTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("شناسه نوع محتوی بار باید بزرگتر از صفر باشد");
    }
}