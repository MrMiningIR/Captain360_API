using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;

public class UpdateCompanyContentTypeNameCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameCommand>
{
    public UpdateCompanyContentTypeNameCommandValidator()
    {


        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");
    }
}