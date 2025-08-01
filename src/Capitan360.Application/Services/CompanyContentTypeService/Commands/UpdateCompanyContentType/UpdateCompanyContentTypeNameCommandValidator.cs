using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public class UpdateCompanyContentTypeNameCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameCommand>
{
    public UpdateCompanyContentTypeNameCommandValidator()
    {
        RuleFor(x => x.OriginalContentId)
            .GreaterThan(0).WithMessage("شناسه نوع محتوی الزامی است");

        RuleFor(x => x.ContentTypeName)
            .MaximumLength(30).WithMessage("نام محتوی نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام محتوی نمی‌تواند کمتر از 3 کاراکتر باشد");
    }
}