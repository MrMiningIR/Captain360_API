using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType
{
    public class UpdateActiveStateCompanyContentTypeCommandValidator : AbstractValidator<UpdateActiveStateCompanyContentTypeCommand>
    {
        public UpdateActiveStateCompanyContentTypeCommandValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
        }
    }
}