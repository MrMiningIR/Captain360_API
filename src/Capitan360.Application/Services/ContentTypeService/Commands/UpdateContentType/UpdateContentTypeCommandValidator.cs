using Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;
using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands;

public class UpdateContentTypeCommandValidator : AbstractValidator<UpdateContentTypeCommand>
{
    public UpdateContentTypeCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ContentTypeName)
            .NotEmpty().WithMessage("نام نوع محتوا نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام نوع محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.ContentTypeDescription)
            .NotEmpty().WithMessage("توضیحات نوع محتوا نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("توضیحات نوع محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد");


    }
}