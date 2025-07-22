using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public class UpdateCompanyContentTypeValidator : AbstractValidator<UpdateCompanyContentTypeCommand>
{
    public UpdateCompanyContentTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع محتوا الزامی است");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع محتوا الزامی است");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");
        RuleFor(x => x.ContentTypeName)
            .MaximumLength(50).WithMessage("نام محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام محتوا نمی‌تواند کمتر از 3 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.ContentTypeName));
    }
}