using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;

public class UpdateCompanyUriCommandValidator : AbstractValidator<UpdateCompanyUriCommand>
{
    public UpdateCompanyUriCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .GreaterThan(0).WithMessage("شناسه URI باید مشخص باشد");



        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("آدرس URI نمی‌تواند خالی باشد")
            .MaximumLength(100).WithMessage("آدرس URI نمی‌تواند بیشتر از 100 کاراکتر باشد")
            ;

        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("توضیحات نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .When(x => string.IsNullOrEmpty(x.Description));
    }
}