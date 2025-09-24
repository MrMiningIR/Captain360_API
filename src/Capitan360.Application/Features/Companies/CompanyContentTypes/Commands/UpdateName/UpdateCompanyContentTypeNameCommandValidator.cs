using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;

internal class UpdateCompanyContentTypeNameCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameCommand>
{
    public UpdateCompanyContentTypeNameCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");
    }
}