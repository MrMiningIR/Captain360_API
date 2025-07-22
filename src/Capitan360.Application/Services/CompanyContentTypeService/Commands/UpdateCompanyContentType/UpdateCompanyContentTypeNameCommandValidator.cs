using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public class UpdateCompanyContentTypeNameCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameCommand>
{
    public UpdateCompanyContentTypeNameCommandValidator()
    {
        RuleFor(x => x.OriginalContentId)
            .GreaterThan(0).WithMessage("شناسه نوع محتوا الزامی است");

        RuleFor(x => x.ContentTypeName)
            .MaximumLength(30).WithMessage("نام محتوا نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام محتوا نمی‌تواند کمتر از 3 کاراکتر باشد");
    }
}