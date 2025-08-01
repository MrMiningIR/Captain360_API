using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public class UpdateCompanyContentTypeCommandValidator : AbstractValidator<UpdateCompanyContentTypeCommand>
{
    public UpdateCompanyContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع محتوی شرکت الزامی است");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع محتوی الزامی است");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");
        RuleFor(x => x.CompanyContentTypeName)
            .MaximumLength(50).WithMessage("نام محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام محتوی نمی‌تواند کمتر از 3 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.CompanyContentTypeName));
    }
}