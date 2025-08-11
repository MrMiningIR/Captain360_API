using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;

public class UpdateCompanyContentTypeNameAndDescriptionCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameAndDescriptionCommand>
{
    public UpdateCompanyContentTypeNameAndDescriptionCommandValidator()
    {

        RuleFor(x => x.Id)
    .GreaterThan(0).WithMessage("شناسه نوع محتوی شرکت الزامی است");



        RuleFor(x => x.CompanyContentTypeName)
            .NotEmpty().WithMessage("نام محتوی الزامی است")
            .MaximumLength(50).WithMessage("نام محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(4).WithMessage("نام محتوی نمی‌تواند کمتر از 4 کاراکتر باشد")
           ;

        RuleFor(x => x.CompanyContentTypeDescription)
            .MaximumLength(500).WithMessage(" توضیحات محتوی نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .MinimumLength(4).WithMessage(" توضیحات محتوی نمی‌تواند کمتر از 4 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.CompanyContentTypeDescription));



    }
}