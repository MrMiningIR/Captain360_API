using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;

public class CreateContentTypeValidator : AbstractValidator<CreateContentTypeCommand>
{
    public CreateContentTypeValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");

        RuleFor(x => x.ContentTypeName)
            .NotEmpty().WithMessage("نام نوع محتوی الزامی است")
            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.ContentTypeDescription)
            .NotEmpty().WithMessage("توضیحات نوع محتوی الزامی است")
            .MaximumLength(50).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}