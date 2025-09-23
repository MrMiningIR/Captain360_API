using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Commands.Update;

public class UpdateContentTypeCommandValidator : AbstractValidator<UpdateContentTypeCommand>
{
    public UpdateContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد"); 

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MinimumLength(4).WithMessage("حداقل طول نام 4 کاراکتر است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");
    }
}