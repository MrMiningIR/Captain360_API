using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;

public class UpdateContentTypeCommandValidator : AbstractValidator<UpdateContentTypeCommand>
{
    public UpdateContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
    .GreaterThan(0).WithMessage("شناسه نوع محتوا باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ContentTypeName)
            .NotEmpty().WithMessage("نام نوع محتوی نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.ContentTypeDescription)

            .MaximumLength(500).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.ContentTypeDescription));
    }
}