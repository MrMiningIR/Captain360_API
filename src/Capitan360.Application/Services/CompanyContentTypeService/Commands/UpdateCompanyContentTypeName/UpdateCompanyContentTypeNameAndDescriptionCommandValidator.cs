using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;

public class UpdateCompanyContentTypeNameAndDescriptionCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameAndDescriptionCommand>
{
    public UpdateCompanyContentTypeNameAndDescriptionCommandValidator()
    {


        RuleFor(x => x.ContentTypeName)
            .MaximumLength(30).WithMessage("نام محتوی نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام محتوی نمی‌تواند کمتر از 3 کاراکتر باشد");
    }
}