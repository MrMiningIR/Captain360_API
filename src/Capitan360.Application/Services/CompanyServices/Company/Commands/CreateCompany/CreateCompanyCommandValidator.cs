using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        //_companyTypeRepository = companyTypeRepository;

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required")
            .MaximumLength(20)
            .WithMessage("Code must not exceed 100 characters")
            .MinimumLength(4)
            .WithMessage("Code must be at least 5 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Matches(@"^\d{11}$")
            .WithMessage("PhoneNumber must be 11 digits");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(5)
            .WithMessage("Name must be at least 6 characters")
            .MaximumLength(50)
            .WithMessage("Code must not exceed 100 characters");

        RuleFor(x => x.CompanyTypeId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("CompanyType is required");

        ;
        RuleFor(x => x.CityId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("CityId is required")
            ;
        RuleFor(x => x.ProvinceId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("ProvinceId is required")
            ;
        RuleFor(x => x.Description)
        .NotEmpty()
        .WithMessage("توضحیحات شرکت الزامی است")
        .MaximumLength(500)
        .WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد")
        .MinimumLength(10)
        .WithMessage("توضحیحات باید حداقل 10 کاراکتر باشد");
    }
}