using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateDescription;

public class UpdateCompanyContentTypeDescriptionCommandValidator : AbstractValidator<UpdateCompanyContentTypeDescriptionCommand>
{
    public UpdateCompanyContentTypeDescriptionCommandValidator()
    {


        RuleFor(x => x.CompanyContentTypeDescription)

            .MinimumLength(5).WithMessage("نام محتوی بار نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(500).WithMessage("نام محتوی بار نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => string.IsNullOrEmpty(x.CompanyContentTypeDescription));
    }
}