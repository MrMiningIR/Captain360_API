using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;

//public class UpdateContentTypeCommandValidator : AbstractValidator<UpdateContentTypeCommand>
//{
//    public UpdateContentTypeCommandValidator()
//    {
//        RuleFor(x => x.CompanyTypeId)
//            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

//        RuleFor(x => x.ContentTypeName)
//            .NotEmpty().WithMessage("نام نوع محتوی نمی‌تواند خالی باشد")
//            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

//        RuleFor(x => x.ContentTypeDescription)
//            .NotEmpty().WithMessage("توضیحات نوع محتوی نمی‌تواند خالی باشد")
//            .MaximumLength(50).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");


//    }
//}

public class UpdateContentTypeCommandValidator : BaseContentTypeValidator<UpdateContentTypeCommand>
{
    public UpdateContentTypeCommandValidator()
    {
        ApplyCommonRules(x => x.CompanyTypeId,
            x => x.ContentTypeName, x => x.ContentTypeDescription);

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الزامی است و باید بزرگ‌تر از صفر باشد");
    }
}