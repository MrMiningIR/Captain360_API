using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;

public class UpdateCompanyContentTypeCommandValidator : AbstractValidator<UpdateCompanyContentTypeCommand>
{
    public UpdateCompanyContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyContentTypeName)
            .NotEmpty().WithMessage("نام محتوی بار الزامی است")
            .MinimumLength(4).WithMessage("نام محتوی بار نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("نام محتوی بار نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CompanyContentTypeDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyContentTypeDescription))
            .WithMessage("توضیحات محتوی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}