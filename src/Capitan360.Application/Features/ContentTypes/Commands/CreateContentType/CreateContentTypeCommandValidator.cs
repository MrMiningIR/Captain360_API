using FluentValidation;

namespace Capitan360.Application.Features.ContentTypeService.Commands.CreateContentType;

public class CreateContentTypeCommandValidator : AbstractValidator<CreateContentTypeCommand>
{
    public CreateContentTypeCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.ContentTypeName)
            .NotEmpty().WithMessage("نام محتوی بار الزامی است")
            .MinimumLength(4).WithMessage("نام محتوی بار نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("نام محتوی بار نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.ContentTypeDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ContentTypeDescription))
            .WithMessage("توضیحات محتوی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}